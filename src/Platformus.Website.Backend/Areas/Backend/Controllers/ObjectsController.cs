// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Data.Entities;
using Platformus.Core.Extensions;
using Platformus.Core.Filters;
using Platformus.Website.Backend.ViewModels.Objects;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageObjectsPermission)]
  public class ObjectsController : Core.Backend.Controllers.ControllerBase
  {
    private IWebHostEnvironment webHostEnvironment;

    private IRepository<int, Class, ClassFilter> ClassRepository
    {
      get => this.Storage.GetRepository<int, Class, ClassFilter>();
    }

    private IRepository<int, Object, ObjectFilter> ObjectRepository
    {
      get => this.Storage.GetRepository<int, Object, ObjectFilter>();
    }

    private IRepository<int, Property, PropertyFilter> PropertyRepository
    {
      get => this.Storage.GetRepository<int, Property, PropertyFilter>();
    }

    private IRepository<int, Relation, RelationFilter> RelationRepository
    {
      get => this.Storage.GetRepository<int, Relation, RelationFilter>();
    }

    public ObjectsController(IStorage storage, IWebHostEnvironment webHostEnvironment)
      : base(storage)
    {
      this.webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> IndexAsync([FromQuery]ObjectFilter filter = null, string orderBy = null, int skip = 0, int take = 10)
    {
      return this.View(await new IndexViewModelFactory().CreateAsync(
        this.HttpContext, filter,
        filter?.Class?.Id == null ? null : await this.ObjectRepository.GetAllAsync(
          filter, orderBy, skip, take,
          new Inclusion<Object>("Properties.Member"),
          new Inclusion<Object>("Properties.StringValue.Localizations")
        ),
        orderBy, skip, take, await this.ObjectRepository.CountAsync(filter)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]ObjectFilter filter, int? id)
    {
      Object @object = id == null ? null : await this.ObjectRepository.GetByIdAsync(
        (int)id,
        new Inclusion<Object>("Properties.Member"),
        new Inclusion<Object>("Properties.StringValue.Localizations"),
        new Inclusion<Object>("PrimaryRelations.Member"),
        new Inclusion<Object>("ForeignRelations.Member")
      );

      return this.View(await new CreateOrEditViewModelFactory().CreateAsync(
        this.HttpContext, filter, @object
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]ObjectFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Object @object = new CreateOrEditViewModelMapper().Map(
          filter,
          createOrEdit.Id == null ? new Object() : await this.ObjectRepository.GetByIdAsync(
            (int)createOrEdit.Id,
            new Inclusion<Object>("Properties.Member"),
            new Inclusion<Object>("Properties.StringValue.Localizations"),
            new Inclusion<Object>("ForeignRelations.Member")
          ),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.ObjectRepository.Create(@object);

        else this.ObjectRepository.Edit(@object);

        await this.Storage.SaveAsync();

        Class @class = await this.ClassRepository.GetByIdAsync(
          (int)filter.Class.Id,
          new Inclusion<Class>(c => c.Tabs),
          new Inclusion<Class>(c => c.Parent.Tabs),
          new Inclusion<Class>("Members.PropertyDataType"),
          new Inclusion<Class>("Members.RelationClass"),
          new Inclusion<Class>("Parent.Members.PropertyDataType"),
          new Inclusion<Class>("Parent.Members.RelationClass")
        );

        await this.CreateOrEditPropertiesAsync(@class, @object);
        await this.CreateOrEditRelationsAsync(filter, @object);

        if (createOrEdit.Id == null)
          Event<IObjectCreatedEventHandler, HttpContext, Object>.Broadcast(this.HttpContext, @object);

        else Event<IObjectEditedEventHandler, HttpContext, Object>.Broadcast(this.HttpContext, @object);

        return this.Redirect(this.Request.CombineUrl("/backend/objects"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      try
      {
        string imagesPath = Path.Combine(this.webHostEnvironment.WebRootPath, "images", "objects", id.ToString());

        Directory.Delete(imagesPath, true);
      }

      catch { }

      Object @object = await this.ObjectRepository.GetByIdAsync(id);

      this.ObjectRepository.Delete(@object.Id);
      await this.Storage.SaveAsync();
      Event<IObjectDeletedEventHandler, HttpContext, Object>.Broadcast(this.HttpContext, @object);
      return this.Redirect(string.Format("/backend/objects?class.id={0}", @object.ClassId));
    }

    private async Task CreateOrEditPropertiesAsync(Class @class, Object @object)
    {
      if (@object.Properties != null)
        await this.DeletePropertiesAsync(@object);

      await this.CreatePropertiesAsync(@class, @object);
    }

    private async Task DeletePropertiesAsync(Object @object)
    {
      foreach (Property property in @object.Properties)
        this.PropertyRepository.Delete(property.Id);

      await this.Storage.SaveAsync();
      @object.Properties = null;
    }

    private async Task CreatePropertiesAsync(Class @class, Object @object)
    {
      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("propertyMember"))
        {
          string memberIdAndCultureCode = key.Replace("propertyMember", string.Empty);
          string memberId = memberIdAndCultureCode.Remove(memberIdAndCultureCode.Length - 2);
          string cultureId = memberIdAndCultureCode.Substring(memberIdAndCultureCode.Length - 2);

          await this.CreatePropertyAsync(@class, @object, int.Parse(memberId), cultureId, this.Request.Form[key]);
        }
      }

      await this.Storage.SaveAsync();
    }

    private async Task CreatePropertyAsync(Class @class, Object @object, int memberId, string cultureId, string value)
    {
      Member member = @class.GetMembers().FirstOrDefault(m => m.Id == memberId);
      Property property = @object.Properties?.FirstOrDefault(p => p.MemberId == memberId) ?? new Property();

      property.ObjectId = @object.Id;
      property.MemberId = memberId;

      if (member.PropertyDataType.StorageDataType == StorageDataType.Integer)
        property.IntegerValue = value.ToIntWithDefaultValue(0);

      else if (member.PropertyDataType.StorageDataType == StorageDataType.Decimal)
        property.DecimalValue = value.ToDecimalWithDefaultValue(0m);

      else if (member.PropertyDataType.StorageDataType == StorageDataType.String)
      {
        if (property.StringValueId == null)
        {
          Dictionary stringValue = new Dictionary();

          this.Storage.GetRepository<int, Dictionary, DictionaryFilter>().Create(stringValue);
          await this.Storage.SaveAsync();
          property.StringValueId = stringValue.Id;
        }

        Localization localization = new Localization();

        localization.DictionaryId = (int)property.StringValueId;
        localization.CultureId = (await this.HttpContext.GetCultureManager().GetCultureAsync(cultureId)).Id;
        localization.Value = value;
        this.Storage.GetRepository<int, Localization, LocalizationFilter>().Create(localization);
      }

      else if (member.PropertyDataType.StorageDataType == StorageDataType.DateTime)
        property.DateTimeValue = value.ToDateTimeWithDefaultValue(System.DateTime.Now);

      if (property.Id == 0)
        this.PropertyRepository.Create(property);

      else
      {
        List<Property> properties = new List<Property>();

        if (@object.Properties != null)
          properties.AddRange(@object.Properties);

        @object.Properties = properties;
      }

      await this.Storage.SaveAsync();
    }

    private async Task CreateOrEditRelationsAsync(ObjectFilter filter, Object @object)
    {
      if (@object.ForeignRelations != null)
        await this.DeleteRelationsAsync(@object);

      await this.CreateRelationsAsync(filter, @object);
    }

    private async Task DeleteRelationsAsync(Object @object)
    {
      foreach (Relation relation in @object.ForeignRelations)
        this.RelationRepository.Delete(relation.Id);

      await this.Storage.SaveAsync();
      @object.ForeignRelations = null;
    }

    private async Task CreateRelationsAsync(ObjectFilter filter, Object @object)
    {
      if (filter.Primary?.Id != null)
      {
        Object primaryObject = await this.Storage.GetRepository<int, Object, ObjectFilter>().GetByIdAsync(
          (int)filter.Primary.Id,
          new Inclusion<Object>(o => o.Class.Members),
          new Inclusion<Object>(o => o.Class.Parent.Members)
        );

        int? memberId = primaryObject.Class.GetMembers().FirstOrDefault(m => m.IsRelationSingleParent == true)?.Id;

        if (memberId != null)
          await this.CreateRelationAsync((int)memberId, (int)filter.Primary.Id, @object.Id);
      }

      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("relationMember") && !string.IsNullOrEmpty(this.Request.Form[key]))
        {
          string memberId = key.Replace("relationMember", string.Empty);
          IEnumerable<int> primaryIds = this.Request.Form[key].ToString().Split(',').Select(id => int.Parse(id));

          foreach (int primaryId in primaryIds)
            await this.CreateRelationAsync(int.Parse(memberId), primaryId, @object.Id);
        }
      }
    }

    private async Task CreateRelationAsync(int memberId, int primaryId, int foreignId)
    {
      Relation relation = new Relation();

      relation.MemberId = memberId;
      relation.PrimaryId = primaryId;
      relation.ForeignId = foreignId;
      this.RelationRepository.Create(relation);
      await this.Storage.SaveAsync();
    }
  }
}