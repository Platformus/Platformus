// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Platformus.Barebone;
using Platformus.Barebone.Primitives;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;
using Platformus.Routing.Data.Entities;
using Platformus.Routing.DataSources;

namespace Platformus.Domain.DataSources
{
  public class PrimaryObjectsDataSource : DataSourceBase
  {
    public override IEnumerable<DataSourceParameterGroup> ParameterGroups =>
      new DataSourceParameterGroup[]
      {
        new DataSourceParameterGroup(
          "General",
          new DataSourceParameter("RelationMemberId", "Relation member", "member"),
          new DataSourceParameter("NestedXPaths", "Nested XPaths", "textBox")
        ),
        new DataSourceParameterGroup(
          "Filtering",
          new DataSourceParameter("EnableFiltering", "Enable filtering", "checkbox"),
          new DataSourceParameter("QueryUrlParameterName", "“Query” URL parameter name", "textBox", "q")
        ),
        new DataSourceParameterGroup(
          "Sorting",
          new DataSourceParameter("SortingMemberId", "Sorting member", "member"),
          new DataSourceParameter(
            "SortingDirection",
            "Sorting direction",
            new Option[]
            {
              new Option("Ascending", "ASC"),
              new Option("Descending", "DESC")
            },
            "radioButtonList",
            "ASC",
            true
          )
        ),
        new DataSourceParameterGroup(
          "Paging",
          new DataSourceParameter("EnablePaging", "Enable paging", "checkbox"),
          new DataSourceParameter("SkipUrlParameterName", "“Skip” URL parameter name", "textBox", "skip"),
          new DataSourceParameter("TakeUrlParameterName", "“Take” URL parameter name", "textBox", "take"),
          new DataSourceParameter("DefaultTake", "Default “Take” URL parameter value", "numericTextBox", "10")
        )
      };

    public override string Description => "Loads primary objects (related to the current page’s one). Supports filtering, sorting, and paging.";

    protected override dynamic GetRawData(IRequestHandler requestHandler, DataSource dataSource)
    {
      IEnumerable<dynamic> results = null;

      if (!this.HasParameter("SortingMemberId") || !this.HasParameter("SortingDirection"))
        results = this.GetUnsortedSerializedObjects(requestHandler);

      else results = this.GetSortedSerializedObjects(requestHandler);

      results = this.LoadNestedObjects(requestHandler, results);
      return results;
    }

    private IEnumerable<dynamic> GetUnsortedSerializedObjects(IRequestHandler requestHandler)
    {
      SerializedObject serializedPage = this.GetPageSerializedObject(requestHandler);
      IEnumerable<SerializedObject> serializedObjects = null;
      Params @params = this.GetParams(requestHandler, false);

      if (this.HasParameter("RelationMemberId"))
        serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
          requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id,
          this.GetIntParameterValue("RelationMemberId"),
          serializedPage.ObjectId,
          @params
        ).ToList();

      else serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
        requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id,
        serializedPage.ObjectId,
        @params
      ).ToList();

      return serializedObjects.Select(so => this.CreateSerializedObjectViewModel(so));
    }

    private IEnumerable<dynamic> GetSortedSerializedObjects(IRequestHandler requestHandler)
    {
      SerializedObject serializedPage = this.GetPageSerializedObject(requestHandler);
      IEnumerable<SerializedObject> serializedObjects = null;
      Params @params = this.GetParams(requestHandler, true);

      if (this.HasParameter("RelationMemberId"))
        serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
          requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id,
          this.GetIntParameterValue("RelationMemberId"),
          serializedPage.ObjectId,
          @params
        ).ToList();

      else serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
        requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id,
        serializedPage.ObjectId,
        @params
      ).ToList();

      return serializedObjects.Select(so => this.CreateSerializedObjectViewModel(so));
    }

    private SerializedObject GetPageSerializedObject(IRequestHandler requestHandler)
    {
      string url = string.Format("/{0}", requestHandler.HttpContext.GetRouteValue("url"));

      return requestHandler.Storage.GetRepository<ISerializedObjectRepository>().WithCultureIdAndUrlPropertyStringValue(
        requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id, url
      );
    }
  }
}