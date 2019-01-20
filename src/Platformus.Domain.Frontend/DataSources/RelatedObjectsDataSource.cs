// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Platformus.Barebone.Parameters;
using Platformus.Barebone.Primitives;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Services.Abstractions;

namespace Platformus.Domain.Frontend.DataSources
{
  public class RelatedObjectsDataSource : DataSourceBase
  {
    public override IEnumerable<ParameterGroup> ParameterGroups =>
      new ParameterGroup[]
      {
        new ParameterGroup(
          "General",
          new Parameter("RelationMemberId", "Relation member", "memberSelector"),
          new Parameter(
            "RelationType",
            "Relation type",
            new Option[]
            {
              new Option("Primary", "Primary"),
              new Option("Foreign", "Foreign")
            },
            "radioButtonList",
            "Primary",
            true
          ),
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

    public override string Description => "Loads foreign objects (related to the current page’s one). Supports filtering, sorting, and paging.";

    protected override dynamic GetData()
    {
      IEnumerable<dynamic> results = null;

      if (!this.HasParameter("SortingMemberId") || !this.HasParameter("SortingDirection"))
        results = this.GetUnsortedSerializedObjects();

      else results = this.GetSortedSerializedObjects();

      results = this.LoadNestedObjects(results);
      return results;
    }

    private IEnumerable<dynamic> GetUnsortedSerializedObjects()
    {
      SerializedObject serializedPage = this.GetPageSerializedObject();
      IEnumerable <SerializedObject> serializedObjects = null;
      int currentCultureId = this.requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id;
      Params @params = this.GetParams(false);

      if (this.GetStringParameterValue("RelationType") == "Primary")
      {
        if (this.HasParameter("RelationMemberId"))
          serializedObjects = this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
            currentCultureId,
            this.GetIntParameterValue("RelationMemberId"),
            serializedPage.ObjectId,
            @params
          ).ToList();

        else serializedObjects = this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
          currentCultureId,
          serializedPage.ObjectId,
          @params
        ).ToList();
      }

      else
      {
        if (this.HasParameter("RelationMemberId"))
          serializedObjects = this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Foreign(
            currentCultureId,
            this.GetIntParameterValue("RelationMemberId"),
            serializedPage.ObjectId,
            @params
          ).ToList();

        else serializedObjects = this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Foreign(
          currentCultureId,
          serializedPage.ObjectId,
          @params
        ).ToList();
      }

      return serializedObjects.Select(so => this.CreateSerializedObjectViewModel(so));
    }

    private IEnumerable<dynamic> GetSortedSerializedObjects()
    {
      SerializedObject serializedPage = this.GetPageSerializedObject();
      IEnumerable<SerializedObject> serializedObjects = null;
      int currentCultureId = this.requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id;
      Params @params = this.GetParams(true);

      if (this.GetStringParameterValue("RelationType") == "Primary")
      {
        if (this.HasParameter("RelationMemberId"))
          serializedObjects = this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
            currentCultureId,
            this.GetIntParameterValue("RelationMemberId"),
            serializedPage.ObjectId,
            @params
          ).ToList();

        else serializedObjects = this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Primary(
          currentCultureId,
          serializedPage.ObjectId,
          @params
        ).ToList();
      }

      else
      {
        if (this.HasParameter("RelationMemberId"))
          serializedObjects = this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Foreign(
            currentCultureId,
            this.GetIntParameterValue("RelationMemberId"),
            serializedPage.ObjectId,
            @params
          ).ToList();

        else serializedObjects = this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().Foreign(
          currentCultureId,
          serializedPage.ObjectId,
          @params
        ).ToList();
      }

      return serializedObjects.Select(so => this.CreateSerializedObjectViewModel(so));
    }

    private SerializedObject GetPageSerializedObject()
    {
      string url = string.Format("/{0}", this.requestHandler.HttpContext.GetRouteValue("url"));

      return this.requestHandler.Storage.GetRepository<ISerializedObjectRepository>().WithCultureIdAndUrlPropertyStringValue(
        this.requestHandler.GetService<ICultureManager>().GetCurrentCulture().Id, url
      );
    }
  }
}