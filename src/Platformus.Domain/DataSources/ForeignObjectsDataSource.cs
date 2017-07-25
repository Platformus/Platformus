// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Platformus.Barebone;
using Platformus.Barebone.Backend;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;

namespace Platformus.Domain.DataSources
{
  public class ForeignObjectsDataSource : DataSourceBase, IMultipleObjectsDataSource
  {
    public override IEnumerable<DataSourceParameterGroup> DataSourceParameterGroups
    {
      get
      {
        return new DataSourceParameterGroup[]
        {
          new DataSourceParameterGroup(
            "General",
            new DataSourceParameter("RelationMemberId", "Relation member", "member"),
            new DataSourceParameter("NestedXPaths", "Nested XPaths", "text")
          ),
          new DataSourceParameterGroup(
            "Filtering",
            new DataSourceParameter("EnableFiltering", "Enable filtering", "checkbox"),
            new DataSourceParameter("QueryUrlParameterName", "“Query” URL parameter name", "text")
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
              "radio",
              true
            )
          ),
          new DataSourceParameterGroup(
            "Paging",
            new DataSourceParameter("EnablePaging", "Enable paging", "checkbox"),
            new DataSourceParameter("SkipUrlParameterName", "“Skip” URL parameter name", "text"),
            new DataSourceParameter("TakeUrlParameterName", "“Take” URL parameter name", "text"),
            new DataSourceParameter("DefaultTake", "Default “Take” URL parameter value", "numeric")
          )
        };
      }
    }

    public IEnumerable<dynamic> GetSerializedObjects(IRequestHandler requestHandler, params KeyValuePair<string, string>[] args)
    {
      IEnumerable<dynamic> results = null;

      if (!this.HasArgument(args, "SortingMemberId") || !this.HasArgument(args, "SortingDirection"))
        results = this.GetUnsortedSerializedObjects(requestHandler, args);

      else results = this.GetSortedSerializedObjects(requestHandler, args);

      results = this.LoadNestedObjects(requestHandler, results, args);
      return results;
    }

    private IEnumerable<dynamic> GetUnsortedSerializedObjects(IRequestHandler requestHandler, params KeyValuePair<string, string>[] args)
    {
      SerializedObject serializedPage = this.GetPageSerializedObject(requestHandler);
      IEnumerable <SerializedObject> serializedObjects = null;
      Params @params = this.GetParams(requestHandler, args, false);

      if (this.HasArgument(args, "RelationMemberId"))
        serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Foreign(
          CultureManager.GetCurrentCulture(requestHandler.Storage).Id,
          this.GetIntArgument(args, "RelationMemberId"),
          serializedPage.ObjectId,
          @params
        ).ToList();

      else serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Foreign(
        CultureManager.GetCurrentCulture(requestHandler.Storage).Id,
        serializedPage.ObjectId,
        @params
      ).ToList();

      return serializedObjects.Select(so => this.CreateSerializedObjectViewModel(so));
    }

    private IEnumerable<dynamic> GetSortedSerializedObjects(IRequestHandler requestHandler, params KeyValuePair<string, string>[] args)
    {
      SerializedObject serializedPage = this.GetPageSerializedObject(requestHandler);
      IEnumerable<SerializedObject> serializedObjects = null;
      Params @params = this.GetParams(requestHandler, args, true);

      if (this.HasArgument(args, "RelationMemberId"))
        serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Foreign(
          CultureManager.GetCurrentCulture(requestHandler.Storage).Id,
          this.GetIntArgument(args, "RelationMemberId"),
          serializedPage.ObjectId,
          @params
        ).ToList();

      else serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Foreign(
        CultureManager.GetCurrentCulture(requestHandler.Storage).Id,
        serializedPage.ObjectId,
        @params
      ).ToList();

      return serializedObjects.Select(so => this.CreateSerializedObjectViewModel(so));
    }

    private SerializedObject GetPageSerializedObject(IRequestHandler requestHandler)
    {
      string url = string.Format("/{0}", requestHandler.HttpContext.GetRouteValue("url"));

      return requestHandler.Storage.GetRepository<ISerializedObjectRepository>().WithCultureIdAndUrlPropertyStringValue(
        CultureManager.GetCurrentCulture(requestHandler.Storage).Id, url
      );
    }
  }
}