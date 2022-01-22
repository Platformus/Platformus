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
using Platformus.Core.Filters;
using Platformus.Website.Backend.ViewModels.Objects;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
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

    private IRepository<int, Dictionary, DictionaryFilter> DictionaryRepository
    {
      get => this.Storage.GetRepository<int, Dictionary, DictionaryFilter>();
    }

    private IRepository<int, Localization, LocalizationFilter> LocalizationRepository
    {
      get => this.Storage.GetRepository<int, Localization, LocalizationFilter>();
    }

    public ObjectsController(IStorage storage, IWebHostEnvironment webHostEnvironment)
      : base(storage)
    {
      this.webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> IndexAsync([FromQuery]ObjectFilter filter = null, string sorting = null, int offset = 0, int limit = 10)
    {
      return this.View(await IndexViewModelFactory.CreateAsync(
        this.HttpContext, filter, sorting, offset, limit, await this.ObjectRepository.CountAsync(filter),
        filter?.Class?.Id == null ? null : await this.ObjectRepository.GetAllAsync(
          filter, sorting, offset, limit,
          new Inclusion<Object>("Properties.StringValue.Localizations")
        )
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]ObjectFilter filter, int? id)
    {
      Object @object = id == null ? null : await this.ObjectRepository.GetByIdAsync(
        (int)id,
        new Inclusion<Object>("Properties.Member.PropertyDataType"),
        new Inclusion<Object>("Properties.StringValue.Localizations"),
        new Inclusion<Object>("PrimaryRelations.Member"),
        new Inclusion<Object>("ForeignRelations.Member")
      );

      return this.View(await CreateOrEditViewModelFactory.CreateAsync(
        this.HttpContext, filter, @object
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]ObjectFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Class @class = await this.ClassRepository.GetByIdAsync(
          (int)filter.Class.Id,
          new Inclusion<Class>(c => c.Tabs),
          new Inclusion<Class>(c => c.Parent.Tabs),
          new Inclusion<Class>("Members.PropertyDataType"),
          new Inclusion<Class>("Members.RelationClass"),
          new Inclusion<Class>("Parent.Members.PropertyDataType"),
          new Inclusion<Class>("Parent.Members.RelationClass")
        );

        Object @object = CreateOrEditViewModelMapper.Map(
          filter,
          createOrEdit.Id == null ? new Object() { Properties = new List<Property>() } : await this.ObjectRepository.GetByIdAsync(
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

        this.MergeProperties(@class, @object);
        await this.MergeRelationsAsync(@object, filter.Primary?.Id);
        await this.Storage.SaveAsync();

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
      return this.Redirect(this.Request.CombineUrl("/backend/objects"));
    }

    private void MergeProperties(Class @class, Object @object)
    {
      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("propertyMember"))
        {
          int memberId;
          string cultureId = null;

          if (char.IsDigit(key[key.Length - 1]))
            memberId = int.Parse(key.Replace("propertyMember", string.Empty));

          else
          {
            memberId = int.Parse(key.Remove(key.Length - 2).Replace("propertyMember", string.Empty));
            cultureId = key.Substring(key.Length - 2);
          }

          this.MergeProperty(@class, @object, memberId, cultureId, this.Request.Form[key]);
        }
      }
    }

    private void MergeProperty(Class @class, Object @object, int memberId, string cultureId, string value)
    {
      Property property = @object.Properties.FirstOrDefault(p => p.MemberId == memberId);

      if (property == null)
      {
        property = new Property();
        property.Object = @object;
        property.MemberId = memberId;
        this.PropertyRepository.Create(property);
        @object.Properties.Add(property);
      }

      else if (property.Id != 0)
        this.PropertyRepository.Edit(property);

      Member member = @class.Members.FirstOrDefault(m => m.Id == memberId) ?? @class.Parent.Members.First(m => m.Id == memberId);

      if (member.PropertyDataType.StorageDataType == StorageDataTypes.Integer)
        property.IntegerValue = value.ToIntWithDefaultValue(0);

      else if (member.PropertyDataType.StorageDataType == StorageDataTypes.Decimal)
        property.DecimalValue = value.ToDecimalWithDefaultValue(0m);

      else if (member.PropertyDataType.StorageDataType == StorageDataTypes.String)
      {
        if (property.StringValue == null)
        {
          property.StringValue = new Dictionary();
          this.DictionaryRepository.Create(property.StringValue);
        }

        Localization localization = property.StringValue.Localizations?.FirstOrDefault(l => l.CultureId == cultureId);

        if (localization == null)
        {
          localization = new Localization();
          localization.Dictionary = property.StringValue;
          localization.CultureId = cultureId;
          localization.Value = value;
          this.LocalizationRepository.Create(localization);
        }

        else
        {
          localization.Value = value;
          this.LocalizationRepository.Edit(localization);
        }
      }

      else if (member.PropertyDataType.StorageDataType == StorageDataTypes.DateTime)
        property.DateTimeValue = value.ToDateTimeWithDefaultValue(System.DateTime.Now);
    }

    private async Task MergeRelationsAsync(Object @object, int? primaryId)
    {
      List<Relation> relations = new List<Relation>();

      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("relationMember") && !string.IsNullOrEmpty(this.Request.Form[key]))
        {
          int memberId = int.Parse(key.Replace("relationMember", string.Empty));

          foreach (int id in this.Request.Form[key].ToString().Split(',').Select(id => int.Parse(id)))
            relations.Add(new Relation() { MemberId = memberId, Foreign = @object, PrimaryId = id });
        }
      }

      if (primaryId != null)
      {
        Object primaryObject = await this.Storage.GetRepository<int, Object, ObjectFilter>().GetByIdAsync(
          (int)primaryId,
          new Inclusion<Object>(o => o.Class.Members),
          new Inclusion<Object>(o => o.Class.Parent.Members)
        );

        int? memberId = primaryObject.Class.GetMembers().FirstOrDefault(m => m.IsRelationSingleParent == true)?.Id;

        if (memberId != null)
          relations.Add(new Relation() { MemberId = (int)memberId, Foreign = @object, PrimaryId = (int)primaryId });
      }
      
      IEnumerable<Relation> currentRelations = @object.ForeignRelations ?? System.Array.Empty<Relation>();

      foreach (Relation relation in currentRelations.Where(cr => !relations.Any(r => r.MemberId == cr.MemberId && r.PrimaryId == cr.PrimaryId)))
        this.RelationRepository.Delete(relation.Id);

      foreach (Relation relation in relations.Where(r => !currentRelations.Any(cr => cr.MemberId == r.MemberId && cr.PrimaryId == r.PrimaryId)))
        this.RelationRepository.Create(relation);
    }
  }
}