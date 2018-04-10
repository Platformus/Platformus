// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Data.EntityFramework.PostgreSql
{
  /// <summary>
  /// Implements the <see cref="ICultureRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Culture"/> entities in the context of PostgreSQL database.
  /// </summary>
  public class CultureRepository : RepositoryBase<Culture>, ICultureRepository
  {
    /// <summary>
    /// Gets the culture by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the culture.</param>
    /// <returns>Found culture with the given identifier.</returns>
    public Culture WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the culture by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the culture.</param>
    /// <returns>Found culture with the given code.</returns>
    public Culture WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(c => string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets the neutral culture.
    /// </summary>
    /// <returns>Found neutral culture.</returns>
    public Culture Neutral()
    {
      return this.dbSet.FirstOrDefault(c => c.IsNeutral);
    }

    /// <summary>
    /// Gets the default culture.
    /// </summary>
    /// <returns>Found default culture.</returns>
    public Culture Default()
    {
      return this.dbSet.FirstOrDefault(c => c.IsFrontendDefault);
    }

    /// <summary>
    /// Gets all the cultures using sorting by name (ascending).
    /// </summary>
    /// <returns>Found cultures.</returns>
    public IEnumerable<Culture> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(c => c.Name);
    }

    /// <summary>
    /// Gets the not neutral cultures using sorting by name (ascending).
    /// </summary>
    /// <returns>Found not neutral cultures.</returns>
    public IEnumerable<Culture> NotNeutral()
    {
      return this.dbSet.AsNoTracking().Where(c => !c.IsNeutral).OrderBy(c => c.Name);
    }

    /// <summary>
    /// Gets all the cultures using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The culture property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of cultures that should be skipped.</param>
    /// <param name="take">The number of cultures that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found cultures using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Culture> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredCultures(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the culture.
    /// </summary>
    /// <param name="culture">The culture to create.</param>
    public void Create(Culture culture)
    {
      this.dbSet.Add(culture);
    }

    /// <summary>
    /// Edits the culture.
    /// </summary>
    /// <param name="culture">The culture to edit.</param>
    public void Edit(Culture culture)
    {
      this.storageContext.Entry(culture).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the culture specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the culture to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the culture.
    /// </summary>
    /// <param name="culture">The culture to delete.</param>
    public void Delete(Culture culture)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM ""SerializedObjects"" WHERE ""CultureId"" = {0};
          DELETE FROM ""SerializedMenus"" WHERE ""CultureId"" = {0};
          DELETE FROM ""SerializedForms"" WHERE ""CultureId"" = {0};
          DELETE FROM ""Localizations"" WHERE ""CultureId"" = {0};
        ",
        culture.Id
      );

      this.dbSet.Remove(culture);
    }

    /// <summary>
    /// Counts the number of the cultures with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of cultures found.</returns>
    public int Count(string filter)
    {
      return this.GetFilteredCultures(dbSet, filter).Count();
    }

    private IQueryable<Culture> GetFilteredCultures(IQueryable<Culture> cultures, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return cultures;

      return cultures.Where(c => c.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}