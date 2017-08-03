// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;

namespace Platformus.Domain.DataSources
{
  public class ObjectsDataSource : DataSourceBase, IMultipleObjectsDataSource
  {
    public override IEnumerable<DataSourceParameterGroup> DataSourceParameterGroups =>
      new DataSourceParameterGroup[]
      {
        new DataSourceParameterGroup(
          "General",
          new DataSourceParameter("ClassId", "Class of the objects to load", "class", null, true),
          new DataSourceParameter("NestedXPaths", "Nested XPaths", "text")
        ),
        new DataSourceParameterGroup(
          "Filtering",
          new DataSourceParameter("EnableFiltering", "Enable filtering", "checkbox"),
          new DataSourceParameter("QueryUrlParameterName", "“Query” URL parameter name", "text", "q")
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
            "ASC",
            true
          )
        ),
        new DataSourceParameterGroup(
          "Paging",
          new DataSourceParameter("EnablePaging", "Enable paging", "checkbox"),
          new DataSourceParameter("SkipUrlParameterName", "“Skip” URL parameter name", "text", "skip"),
          new DataSourceParameter("TakeUrlParameterName", "“Take” URL parameter name", "text", "take"),
          new DataSourceParameter("DefaultTake", "Default “Take” URL parameter value", "numeric", "10")
        )
      };

    public override string Description => "Loads objects of the given class. Supports filtering, sorting, and paging.";

    public IEnumerable<dynamic> GetSerializedObjects(IRequestHandler requestHandler, params KeyValuePair<string, string>[] args)
    {
      if (!this.HasArgument(args, "ClassId"))
        return new SerializedObject[] { };

      IEnumerable<dynamic> results = null;

      if (!this.HasArgument(args, "SortingMemberId") || !this.HasArgument(args, "SortingDirection"))
        results = this.GetUnsortedSerializedObjects(requestHandler, args);

      else results = this.GetSortedSerializedObjects(requestHandler, args);

      results = this.LoadNestedObjects(requestHandler, results, args);
      return results;
    }

    private IEnumerable<dynamic> GetUnsortedSerializedObjects(IRequestHandler requestHandler, params KeyValuePair<string, string>[] args)
    {
      IEnumerable<SerializedObject> serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().FilteredByCultureIdAndClassId(
        CultureManager.GetCurrentCulture(requestHandler.Storage).Id,
        this.GetIntArgument(args, "ClassId"),
        this.GetParams(requestHandler, args, false)
      ).ToList();

      return serializedObjects.Select(so => this.CreateSerializedObjectViewModel(so));
    }

    private IEnumerable<dynamic> GetSortedSerializedObjects(IRequestHandler requestHandler, params KeyValuePair<string, string>[] args)
    {
      IEnumerable<SerializedObject> serializedObjects = requestHandler.Storage.GetRepository<ISerializedObjectRepository>().FilteredByCultureIdAndClassId(
        CultureManager.GetCurrentCulture(requestHandler.Storage).Id,
        this.GetIntArgument(args, "ClassId"),
        this.GetParams(requestHandler, args, true)
      ).ToList();

      return serializedObjects.Select(so => this.CreateSerializedObjectViewModel(so));
    }
  }
}