// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone.Parameters;
using Platformus.Barebone.Primitives;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;

namespace Platformus.Domain.Frontend.DataSources
{
  public class ObjectsDataSource : DataSourceBase
  {
    public override IEnumerable<ParameterGroup> ParameterGroups =>
      new ParameterGroup[]
      {
        new ParameterGroup(
          "General",
          new Parameter("ClassId", "Class of the objects to load", "classSelector", null, true),
          new Parameter("NestedXPaths", "Nested XPaths", "textBox")
        ),
        new ParameterGroup(
          "Filtering",
          new Parameter("EnableFiltering", "Enable filtering", "checkbox"),
          new Parameter("QueryUrlParameterName", "“Query” URL parameter name", "textBox", "q")
        ),
        new ParameterGroup(
          "Sorting",
          new Parameter("SortingMemberId", "Sorting member", "memberSelector"),
          new Parameter(
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
        new ParameterGroup(
          "Paging",
          new Parameter("EnablePaging", "Enable paging", "checkbox"),
          new Parameter("SkipUrlParameterName", "“Skip” URL parameter name", "textBox", "skip"),
          new Parameter("TakeUrlParameterName", "“Take” URL parameter name", "textBox", "take"),
          new Parameter("DefaultTake", "Default “Take” URL parameter value", "numericTextBox", "10")
        )
      };

    public override string Description => "Loads objects of the given class. Supports filtering, sorting, and paging.";

    protected override dynamic GetData()
    {
      if (!this.HasParameter("ClassId"))
        return new SerializedObject[] { };

      IEnumerable<dynamic> results = null;

      if (!this.HasParameter("SortingMemberId") || !this.HasParameter("SortingDirection"))
        results = this.GetUnsortedSerializedObjects();

      else results = this.GetSortedSerializedObjects();

      results = this.LoadNestedObjects(results);
      return results;
    }

    private IEnumerable<dynamic> GetUnsortedSerializedObjects()
    {
      IEnumerable<SerializedObject> serializedObjects = this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().FilteredByCultureIdAndClassId(
        this.requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id,
        this.GetIntParameterValue("ClassId"),
        this.GetParams(false)
      ).ToList();

      return serializedObjects.Select(so => this.CreateSerializedObjectViewModel(so));
    }

    private IEnumerable<dynamic> GetSortedSerializedObjects()
    {
      IEnumerable<SerializedObject> serializedObjects = this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().FilteredByCultureIdAndClassId(
        this.requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id,
        this.GetIntParameterValue("ClassId"),
        this.GetParams(true)
      ).ToList();

      return serializedObjects.Select(so => this.CreateSerializedObjectViewModel(so));
    }
  }
}