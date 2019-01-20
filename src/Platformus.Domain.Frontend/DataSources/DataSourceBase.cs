// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Domain.Services.Abstractions;
using Platformus.Globalization.Services.Abstractions;

namespace Platformus.Domain.Frontend.DataSources
{
  public abstract class DataSourceBase : Platformus.Routing.DataSources.DataSourceBase
  {
    protected Params GetParams(bool enableSorting)
    {
      string filteringQuery = null;

      if (this.HasParameter("EnableFiltering") && this.GetBoolParameterValue("EnableFiltering"))
        filteringQuery = this.requestHandler.HttpContext.Request.Query[this.GetStringParameterValue("QueryUrlParameterName")];

      int? sortingMemberId = null;
      string sortingDirection = null;

      if (enableSorting)
      {
        sortingMemberId = this.GetIntParameterValue("SortingMemberId");
        sortingDirection = this.GetStringParameterValue("SortingDirection");
      }

      int? pagingSkip = null;
      int? pagingTake = null;

      if (this.HasParameter("EnablePaging") && this.GetBoolParameterValue("EnablePaging"))
      {
        int.TryParse(this.requestHandler.HttpContext.Request.Query[this.GetStringParameterValue("SkipUrlParameterName")], out int tempSkip);
        int.TryParse(this.requestHandler.HttpContext.Request.Query[this.GetStringParameterValue("TakeUrlParameterName")], out int tempTake);

        pagingSkip = tempSkip;
        pagingTake = tempTake;

        if (pagingTake == 0)
          pagingTake = this.GetIntParameterValue("DefaultTake");
      }

      return new ParamsFactory(this.requestHandler).Create(filteringQuery, sortingMemberId, sortingDirection, pagingSkip, pagingTake);
    }

    protected dynamic CreateSerializedObjectViewModel(SerializedObject serializedObject)
    {
      ExpandoObjectBuilder expandoObjectBuilder = new ExpandoObjectBuilder();

      expandoObjectBuilder.AddProperty("Id", serializedObject.ObjectId);
      expandoObjectBuilder.AddProperty("ClassId", serializedObject.ClassId);

      foreach (SerializedProperty serializedProperty in JsonConvert.DeserializeObject<IEnumerable<SerializedProperty>>(serializedObject.SerializedProperties))
      {
        if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.Integer)
          expandoObjectBuilder.AddProperty(serializedProperty.Member.Code, serializedProperty.IntegerValue);

        else if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.Decimal)
          expandoObjectBuilder.AddProperty(serializedProperty.Member.Code, serializedProperty.DecimalValue);

        else if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.String)
          expandoObjectBuilder.AddProperty(serializedProperty.Member.Code, serializedProperty.StringValue);

        else if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.DateTime)
          expandoObjectBuilder.AddProperty(serializedProperty.Member.Code, serializedProperty.DateTimeValue);
      }

      return expandoObjectBuilder.Build();
    }

    protected IEnumerable<dynamic> LoadNestedObjects(IEnumerable<dynamic> objects)
    {
      if (!this.HasParameter("NestedXPaths"))
        return objects;

      string nestedXPaths = this.GetStringParameterValue("NestedXPaths");

      if (string.IsNullOrEmpty(nestedXPaths))
        return objects;

      foreach (string nestedXPath in nestedXPaths.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
        objects = this.LoadNestedObjects(objects, nestedXPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList(), 0);

      return objects;
    }

    private IEnumerable<dynamic> LoadNestedObjects(IEnumerable<dynamic> objects, List<string> xPathSegments, int xPathSegmentIndex)
    {
      if (objects.Count() == 0)
        return objects;

      int classId = objects.First().ClassId;
      string xPathSegment = xPathSegments[xPathSegmentIndex];

      foreach (Member member in this.requestHandler.GetService<IDomainManager>().GetMembersByClassIdInlcudingParent(classId))
      {
        if (member.RelationClassId != null && string.Equals(member.Code, xPathSegment, StringComparison.OrdinalIgnoreCase))
        {
          List<dynamic> temp = new List<dynamic>();

          foreach (dynamic @object in objects)
          {
            IEnumerable<dynamic> nestedObjects = this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
              this.requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id, member.Id, (int)@object.Id
            ).ToList().Select(so => this.CreateSerializedObjectViewModel(so));

            if (xPathSegments.Count() > xPathSegmentIndex + 1)
              nestedObjects = this.LoadNestedObjects(nestedObjects, xPathSegments, xPathSegmentIndex + 1);

            new ExpandoObjectBuilder(@object).AddProperty(member.Code, nestedObjects);
            temp.Add(@object);
          }

          @objects = temp;
        }
      }

      return objects;
    }
  }
}