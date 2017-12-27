// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="ICatalogRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Catalog"/> entities in the context of SQL Server database.
  /// </summary>
  public class CatalogRepository : RepositoryBase<Catalog>, ICatalogRepository
  {
    /// <summary>
    /// Gets the catalog by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the catalog.</param>
    /// <returns>Found catalog with the given identifier.</returns>
    public Catalog WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the catalogs filtered by the parent catalog identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="catalogId">The unique identifier of the parent catalog these catalogs belongs to.</param>
    /// <returns>Found catalogs.</returns>
    public IEnumerable<Catalog> FilteredByCatalogId(int? catalogId)
    {
      return this.dbSet.AsNoTracking().Where(c => c.CatalogId == catalogId).OrderBy(c => c.Position);
    }

    /// <summary>
    /// Creates the catalog.
    /// </summary>
    /// <param name="catalog">The catalog to create.</param>
    public void Create(Catalog catalog)
    {
      this.dbSet.Add(catalog);
    }

    /// <summary>
    /// Edits the catalog.
    /// </summary>
    /// <param name="catalog">The catalog to edit.</param>
    public void Edit(Catalog catalog)
    {
      this.storageContext.Entry(catalog).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the catalog specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the catalog to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the catalog.
    /// </summary>
    /// <param name="catalog">The catalog to delete.</param>
    public void Delete(Catalog catalog)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          CREATE TABLE #Catalogs (Id INT PRIMARY KEY);
          WITH X AS (
            SELECT Id FROM Catalogs WHERE CatalogId = {0}
            UNION ALL
            SELECT Catalogs.Id FROM Catalogs INNER JOIN X ON Catalogs.CatalogId = X.Id
          )
          INSERT INTO #Catalogs SELECT Id FROM X;
          CREATE TABLE #Dictionaries (Id INT PRIMARY KEY);
          INSERT INTO #Dictionaries VALUES ({1});
          INSERT INTO #Dictionaries SELECT NameId FROM Catalogs WHERE Id IN (SELECT Id FROM #Catalogs);
          DELETE FROM Catalogs WHERE Id IN (SELECT Id FROM #Catalogs);
          DELETE FROM Catalogs WHERE Id = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM #Dictionaries);
        ",
        catalog.Id,
        catalog.NameId
      );
    }
  }
}