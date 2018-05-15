// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IFeatureRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Feature"/> entities in the context of SQL Server database.
  /// </summary>
  public class FeatureRepository : RepositoryBase<Feature>, IFeatureRepository
  {
    /// <summary>
    /// Gets the feature by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the feature.</param>
    /// <returns>Found feature with the given identifier.</returns>
    public Feature WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the feature by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the feature.</param>
    /// <returns>Found feature with the given code.</returns>
    public Feature WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(f => string.Equals(f.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the features using sorting by position (ascending).
    /// </summary>
    /// <returns>Found features.</returns>
    public IEnumerable<Feature> All()
    {
      return this.dbSet.OrderBy(f => f.Code);
    }

    /// <summary>
    /// Gets all the features using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The feature property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of features that should be skipped.</param>
    /// <param name="take">The number of features that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found features using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Feature> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredFeatures(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the feature.
    /// </summary>
    /// <param name="feature">The feature to create.</param>
    public void Create(Feature feature)
    {
      this.dbSet.Add(feature);
    }

    /// <summary>
    /// Edits the feature.
    /// </summary>
    /// <param name="feature">The feature to edit.</param>
    public void Edit(Feature feature)
    {
      this.storageContext.Entry(feature).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the feature specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the feature to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the feature.
    /// </summary>
    /// <param name="feature">The feature to delete.</param>
    public void Delete(Feature feature)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          CREATE TABLE #Dictionaries (Id INT PRIMARY KEY);
          INSERT INTO #Dictionaries SELECT NameId FROM Features WHERE Id = {0};
          DELETE FROM Features WHERE Id = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM #Dictionaries);
        ",
        feature.Id
      );
    }

    /// <summary>
    /// Counts the number of the features with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of features found.</returns>
    public int Count(string filter)
    {
      return this.GetFilteredFeatures(dbSet, filter).Count();
    }

    private IQueryable<Feature> GetFilteredFeatures(IQueryable<Feature> features, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return features;

      return features.Where(f => f.Code.ToLower().Contains(filter.ToLower()));
    }
  }
}