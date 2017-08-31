// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;

namespace Platformus.Domain.DataSources
{
  public abstract class DataSourceBase : Platformus.Routing.DataSources.DataSourceBase
  {
    protected Params GetParams(IRequestHandler requestHandler, bool enableSorting)
    {
      Filtering filtering = null;

      if (this.HasParameter("EnableFiltering") && this.GetBoolParameterValue("EnableFiltering"))
      {
        filtering = new Filtering(requestHandler.HttpContext.Request.Query[this.GetStringParameterValue("QueryUrlParameterName")]);
      }

      Sorting sorting = null;

      if (enableSorting)
      {
        int sortingMemberId = this.GetIntParameterValue("SortingMemberId");
        string sortingDirection = this.GetStringParameterValue("SortingDirection");
        IDomainManager domainManager = requestHandler.GetService<IDomainManager>();
        Member member = domainManager.GetMember(sortingMemberId);
        DataType dataType = domainManager.GetDataType((int)member.PropertyDataTypeId);

        sorting = new Sorting(dataType.StorageDataType, sortingMemberId, sortingDirection);
      }

      Paging paging = null;

      if (this.HasParameter("EnablePaging") && this.GetBoolParameterValue("EnablePaging"))
      {
        int.TryParse(requestHandler.HttpContext.Request.Query[this.GetStringParameterValue("SkipUrlParameterName")], out int skip);
        int.TryParse(requestHandler.HttpContext.Request.Query[this.GetStringParameterValue("TakeUrlParameterName")], out int take);

        if (take == 0)
          take = this.GetIntParameterValue("DefaultTake");

        paging = new Paging(skip, take);
      }

      return new Params(filtering, sorting, paging);
    }

    protected dynamic CreateSerializedObjectViewModel(SerializedObject serializedObject)
    {
      ViewModelBuilder viewModelBuilder = new ViewModelBuilder();

      viewModelBuilder.BuildId(serializedObject.ObjectId);
      viewModelBuilder.BuildClassId(serializedObject.ClassId);

      foreach (SerializedProperty serializedProperty in JsonConvert.DeserializeObject<IEnumerable<SerializedProperty>>(serializedObject.SerializedProperties))
      {
        if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.Integer)
          viewModelBuilder.BuildProperty(serializedProperty.Member.Code, serializedProperty.IntegerValue);

        else if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.Decimal)
          viewModelBuilder.BuildProperty(serializedProperty.Member.Code, serializedProperty.DecimalValue);

        else if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.String)
          viewModelBuilder.BuildProperty(serializedProperty.Member.Code, serializedProperty.StringValue);

        else if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataType.DateTime)
          viewModelBuilder.BuildProperty(serializedProperty.Member.Code, serializedProperty.DateTimeValue);
      }

      return viewModelBuilder.Build();
    }

    protected IEnumerable<dynamic> LoadNestedObjects(IRequestHandler requestHandler, IEnumerable<dynamic> objects)
    {
      if (!this.HasParameter("NestedXPaths"))
        return objects;

      string nestedXPaths = this.GetStringParameterValue("NestedXPaths");

      if (string.IsNullOrEmpty(nestedXPaths))
        return objects;

      foreach (string nestedXPath in nestedXPaths.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
        objects = this.LoadNestedObjects(requestHandler, objects, nestedXPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList(), 0);

      return objects;
    }

    private IEnumerable<dynamic> LoadNestedObjects(IRequestHandler requestHandler, IEnumerable<dynamic> objects, List<string> xPathSegments, int xPathSegmentIndex)
    {
      if (objects.Count() == 0)
        return objects;

      int classId = objects.First().ClassId;
      string xPathSegment = xPathSegments[xPathSegmentIndex];

      foreach (Member member in requestHandler.GetService<IDomainManager>().GetMembersByClassIdInlcudingParent(classId))
      {
        if (member.RelationClassId != null && string.Equals(member.Code, xPathSegment, StringComparison.OrdinalIgnoreCase))
        {
          List<dynamic> temp = new List<dynamic>();

          foreach (dynamic @object in objects)
          {
            IEnumerable<dynamic> nestedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
              requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id, member.Id, (int)@object.Id
            ).ToList().Select(so => this.CreateSerializedObjectViewModel(so));

            if (xPathSegments.Count() > xPathSegmentIndex + 1)
              nestedObjects = this.LoadNestedObjects(requestHandler, nestedObjects, xPathSegments, xPathSegmentIndex + 1);

            new ViewModelBuilder(@object).BuildProperty(member.Code, nestedObjects);
            temp.Add(@object);
          }

          @objects = temp;
        }
      }

      return objects;
    }
  }
}