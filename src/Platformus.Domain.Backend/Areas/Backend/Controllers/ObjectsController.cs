// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Domain.Backend.ViewModels.Objects;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Domain.Events;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Domain.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseObjectsPermission)]
  public class ObjectsController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public IHostingEnvironment hostingEnvironment;

    public ObjectsController(IStorage storage, IHostingEnvironment hostingEnvironment)
      : base(storage)
    {
      this.hostingEnvironment = hostingEnvironment;
    }

    public IActionResult Index(int? classId, int? objectId, string orderBy = null, string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(classId, objectId, orderBy, direction, skip, take, filter));
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

        if (createOrEdit.Id == null)
          Event<IObjectCreatedEventHandler, IRequestHandler, Object>.Broadcast(this, @object);

        else Event<IObjectEditedEventHandler, IRequestHandler, Object>.Broadcast(this, @object);

        return this.Redirect(this.Request.CombineUrl("/backend/objects"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      try
      {
        string imagesPath = Path.Combine(this.hostingEnvironment.WebRootPath, "images", "objects", id.ToString());

        Directory.Delete(imagesPath, true);
      }

      catch { }

      Object @object = this.Storage.GetRepository<IObjectRepository>().WithKey(id);

      this.Storage.GetRepository<IObjectRepository>().Delete(@object);
      this.Storage.Save();
      Event<IObjectDeletedEventHandler, IRequestHandler, Object>.Broadcast(this, @object);
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
      Member member = this.Storage.GetRepository<IMemberRepository>().WithKey(memberId);
      DataType dataType = this.Storage.GetRepository<IDataTypeRepository>().WithKey((int)member.PropertyDataTypeId);

      if (dataType.StorageDataType == StorageDataType.Integer)
        this.CreateIntegerProperty(property, objectId, memberId, value);

      else if (dataType.StorageDataType == StorageDataType.Decimal)
        this.CreateDecimalProperty(property, objectId, memberId, value);

      else if (dataType.StorageDataType == StorageDataType.String)
        this.CreateStringProperty(property, objectId, memberId, cultureCode, value, dataType.JavaScriptEditorClassName == "image");

      else if (dataType.StorageDataType == StorageDataType.DateTime)
        this.CreateDateTimeProperty(property, objectId, memberId, value);
    }

    private void CreateIntegerProperty(Property property, int objectId, int memberId, string value)
    {
      property = new Property() { ObjectId = objectId, MemberId = memberId };
      property.IntegerValue = value.ToIntWithDefaultValue(0);
      this.Storage.GetRepository<IPropertyRepository>().Create(property);
      this.Storage.Save();
    }

    private void CreateDecimalProperty(Property property, int objectId, int memberId, string value)
    {
      property = new Property() { ObjectId = objectId, MemberId = memberId };
      property.DecimalValue = value.ToDecimalWithDefaultValue(0m);
      this.Storage.GetRepository<IPropertyRepository>().Create(property);
      this.Storage.Save();
    }

    private void CreateStringProperty(Property property, int objectId, int memberId, string cultureCode, string value, bool isImage)
    {
      if (property == null)
      {
        Dictionary stringValue = new Dictionary();

        this.Storage.GetRepository<IDictionaryRepository>().Create(stringValue);
        this.Storage.Save();
        property = new Property() { ObjectId = objectId, MemberId = memberId };
        property.StringValueId = stringValue.Id;
        this.Storage.GetRepository<IPropertyRepository>().Create(property);
        this.Storage.Save();
      }

      Localization localization = new Localization();

      localization.DictionaryId = (int)property.StringValueId;
      localization.CultureId = this.Storage.GetRepository<ICultureRepository>().WithCode(cultureCode).Id;

      // TODO: we must do that better
      if (isImage)
        value = this.MoveImageToValidObjectPath(objectId, value);

      localization.Value = value;
      this.Storage.GetRepository<ILocalizationRepository>().Create(localization);
      this.Storage.Save();
    }

    private string MoveImageToValidObjectPath(int objectId, string imageUrl)
    {
      imageUrl = imageUrl.Replace('/', '\\');

      string sourceImageFilepath = this.hostingEnvironment.WebRootPath + imageUrl.Replace('\\', Path.DirectorySeparatorChar);
      string imageFilename = Path.GetFileName(sourceImageFilepath);
      string destinationImageFilepath = Path.Combine(this.hostingEnvironment.WebRootPath, "images", "objects", objectId.ToString(), imageFilename);

      if (sourceImageFilepath == destinationImageFilepath)
        return imageUrl;

      try
      {
        Directory.CreateDirectory(Path.GetDirectoryName(destinationImageFilepath));
        System.IO.File.Move(sourceImageFilepath, destinationImageFilepath);
      }

      catch { }

      return "/images/objects/" + objectId + "/" + imageFilename;
    }

    private void CreateDateTimeProperty(Property property, int objectId, int memberId, string value)
    {
      property = new Property() { ObjectId = objectId, MemberId = memberId };
      property.DateTimeValue = value.ToDateTimeWithDefaultValue(System.DateTime.Now);
      this.Storage.GetRepository<IPropertyRepository>().Create(property);
      this.Storage.Save();
    }

    private void CreateOrEditRelations(Object @object)
    {
      this.DeleteRelations(@object);
      this.CreateRelations(@object);
    }

    private void DeleteRelations(Object @object)
    {
      foreach (Relation relation in this.Storage.GetRepository<IRelationRepository>().FilteredByForeignId(@object.Id).ToList())
      {
        Member member = this.Storage.GetRepository<IMemberRepository>().WithKey(relation.MemberId);

        if (member.IsRelationSingleParent != true)
          this.Storage.GetRepository<IRelationRepository>().Delete(relation);
      }

      this.Storage.Save();
    }

    private void CreateRelations(Object @object)
    {
      if (!string.IsNullOrEmpty(this.Request.Query["objectId"]))
      {
        int foreignId = int.Parse(this.Request.Query["objectId"]);
        int primaryId = @object.Id;
        int? memberId = this.GetRelationSingleParentMemberId(
          this.Storage.GetRepository<IObjectRepository>().WithKey(foreignId).ClassId, @object.ClassId
        );

        if (memberId != null)
          this.CreateRelation((int)memberId, primaryId, foreignId);
      }

      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("relationMember") && !string.IsNullOrEmpty(this.Request.Form[key]))
        {
          string memberId = key.Replace("relationMember", string.Empty);
          IEnumerable<int> primaryIds = this.Request.Form[key].ToString().Split(',').Select(id => int.Parse(id));

          foreach (int primaryId in primaryIds)
            this.CreateRelation(int.Parse(memberId), primaryId, @object.Id);
        }
      }
    }

    private int? GetRelationSingleParentMemberId(int classId, int relationClassId)
    {
      foreach (Member member in this.Storage.GetRepository<IMemberRepository>().FilteredByClassIdRelationSingleParent(classId).ToList())
        if (member.RelationClassId == relationClassId)
          return member.Id;

      return null;
    }

    private void CreateRelation(int memberId, int primaryId, int foreignId)
    {
      Relation relation = new Relation();

      relation.MemberId = memberId;
      relation.PrimaryId = primaryId;
      relation.ForeignId = foreignId;
      this.Storage.GetRepository<IRelationRepository>().Create(relation);
      this.Storage.Save();
    }
  }
}