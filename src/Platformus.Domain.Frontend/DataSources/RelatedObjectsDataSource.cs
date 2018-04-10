// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Platformus.Barebone.Primitives;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization;
using Platformus.Routing.DataSources;

namespace Platformus.Domain.Frontend.DataSources
{
  public class RelatedObjectsDataSource : DataSourceBase
  {
    public override IEnumerable<DataSourceParameterGroup> ParameterGroups =>
      new DataSourceParameterGroup[]
      {
        new DataSourceParameterGroup(
          "General",
          new DataSourceParameter("RelationMemberId", "Relation member", "member"),
          new DataSourceParameter(
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