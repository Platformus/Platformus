// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Domain.Backend.ViewModels.Objects;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Domain.Backend.Controllers
{
  [Area("Backend")]
  public class ObjectsController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public ObjectsController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(int? classId, string orderBy = "url", string direction = "asc", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelFactory(this).Create(classId, orderBy, direction, skip, take));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public IActionResult CreateOrEdit(int? id, int? classId, int? objectId)
    {
      return this.View(new CreateOrEditViewModelFactory(this).Create(id, classId, objectId));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public IActionResult CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Object @object = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IObjectRepository>().Create(@object);

        else this.Storage.GetRepository<IObjectRepository>().Edit(@object);

        this.Storage.Save();
        this.CreateOrEditProperties(@object);
        this.CreateOrEditRelations(@object);      
        new CacheManager(this).CacheObject(@object);
        return this.Redirect(this.Request.CombineUrl("/backend/objects"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Object @object = this.Storage.GetRepository<IObjectRepository>().WithKey(id);

      this.Storage.GetRepository<IObjectRepository>().Delete(@object);
      this.Storage.Save();
      return this.Redirect(string.Format("/backend/objects?classid={0}", @object.ClassId));
    }

    private void CreateOrEditProperties(Object @object)
    {
      this.DeleteProperties(@object);
      this.CreateProperties(@object);
    }

    private void DeleteProperties(Object @object)
    {
      foreach (Property property in this.Storage.GetRepository<IPropertyRepository>().FilteredByObjectId(@object.Id).ToList())
        this.Storage.GetRepository<IPropertyRepository>().Delete(property);

      this.Storage.Save();
    }

    private void CreateProperties(Object @object)
    {
      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("propertyMember"))
        {
          string memberIdAndCultureCode = key.Replace("propertyMember", string.Empty);
          string memberId = memberIdAndCultureCode.Remove(memberIdAndCultureCode.Length - 2);
          string cultureCode = memberIdAndCultureCode.Substring(memberIdAndCultureCode.Length - 2);

          this.CreateProperty(@object.Id, int.Parse(memberId), cultureCode, this.Request.Form[key]);
        }
      }

      this.Storage.Save();
    }

    private void CreateProperty(int objectId, int memberId, string cultureCode, string value)
    {
      Property property = this.Storage.GetRepository<IPropertyRepository>().WithObjectIdAndMemberId(objectId, memberId);

      if (property == null)
      {
        Dictionary html = new Dictionary();

        this.Storage.GetRepository<IDictionaryRepository>().Create(html);
        this.Storage.Save();
        property = new Property();
        property.ObjectId = objectId;
        property.MemberId = memberId;
        property.HtmlId = html.Id;
        this.Storage.GetRepository<IPropertyRepository>().Create(property);
        this.Storage.Save();
      }

      Localization localization = new Localization();

      localization.DictionaryId = property.HtmlId;
      localization.CultureId = this.Storage.GetRepository<ICultureRepository>().WithCode(cultureCode).Id;
      localization.Value = value;
      this.Storage.GetRepository<ILocalizationRepository>().Create(localization);
    }

    private void CreateOrEditRelations(Object @object)
    {
      this.DeleteRelations(@object);
      this.CreateRelations(@object);
    }

    private void DeleteRelations(Object @object)
    {
      foreach (Relation relation in this.Storage.GetRepository<IRelationRepository>().FilteredByForeignId(@object.Id))
        this.Storage.GetRepository<IRelationRepository>().Delete(relation);

      this.Storage.Save();
    }

    private void CreateRelations(Object @object)
    {
      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("relationMember") && !string.IsNullOrEmpty(this.Request.Form[key]))
        {
          string memberId = key.Replace("relationMember", string.Empty);
          IEnumerable<int> primaryIds = this.Request.Form[key].ToString().Split(',').Select(id => int.Parse(id));

          foreach (int primaryId in primaryIds)
            this.CreateRelation(int.Parse(memberId), primaryId, @object.Id);

          this.Storage.Save();
        }
      }
    }

    private void CreateRelation(int memberId, int primaryId, int foreignId)
    {
      Relation relation = new Relation();

      relation.MemberId = memberId;
      relation.PrimaryId = primaryId;
      relation.ForeignId = foreignId;
      this.Storage.GetRepository<IRelationRepository>().Create(relation);
    }
  }
}