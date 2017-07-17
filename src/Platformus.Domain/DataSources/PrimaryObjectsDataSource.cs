// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;

namespace Platformus.Domain.DataSources
{
  public class PrimaryObjectsDataSource : DataSourceBase, IMultipleObjectsDataSource
  {
    public override IEnumerable<DataSourceParameter> DataSourceParameters
    {
      get
      {
        return new DataSourceParameter[]
        {
          new DataSourceParameter("RelationMemberId", "Relation member ID", "temp"),
          new DataSourceParameter("SortingMemberId", "Sorting member ID", "temp"),
          new DataSourceParameter("SortingDirection", "Sorting direction", "temp"),
          new DataSourceParameter("NestedXPaths", "Nested XPaths", "temp")
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
      IEnumerable<SerializedObject> serializedObjects = null;

      if (this.HasArgument(args, "RelationMemberId"))
        serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
          CultureManager.GetCurrentCulture(requestHandler.Storage).Id,
          this.GetIntArgument(args, "RelationMemberId"),
          serializedPage.ObjectId
        ).ToList();

      serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
        CultureManager.GetCurrentCulture(requestHandler.Storage).Id,
        serializedPage.ObjectId
      ).ToList();

      return serializedObjects.Select(so => this.CreateSerializedObjectViewModel(so));
    }

    private IEnumerable<dynamic> GetSortedSerializedObjects(IRequestHandler requestHandler, params KeyValuePair<string, string>[] args)
    {
      SerializedObject serializedPage = this.GetPageSerializedObject(requestHandler);
      IEnumerable<SerializedObject> serializedObjects = null;
      Params @params = this.GetParams(requestHandler, args);

      if (this.HasArgument(args, "RelationMemberId"))
        serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
          CultureManager.GetCurrentCulture(requestHandler.Storage).Id,
          this.GetIntArgument(args, "RelationMemberId"),
          serializedPage.ObjectId,
          @params
        ).ToList();

      serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
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