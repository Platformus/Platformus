// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.PostgreSql
{
  /// <summary>
  /// Implements the <see cref="ICategoryRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Category"/> entities in the context of PostgreSQL database.
  /// </summary>
  public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
  {
    /// <summary>
    /// Gets the category by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the category.</param>
    /// <returns>Found category with the given identifier.</returns>
    public Category WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the categories filtered by the parent category identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="categoryId">The unique identifier of the parent category these categories belongs to.</param>
    /// <returns>Found categories.</returns>
    public IEnumerable<Category> FilteredByCategoryId(int? categoryId)
    {
      return this.dbSet.AsNoTracking().Where(c => c.CategoryId == categoryId).OrderBy(c => c.Position);
    }

    /// <summary>
    /// Creates the category.
    /// </summary>
    /// <param name="category">The category to create.</param>
    public void Create(Category category)
    {
      this.dbSet.Add(category);
    }

    /// <summary>
    /// Edits the category.
    /// </summary>
    /// <param name="category">The category to edit.</param>
    public void Edit(Category category)
    {
      this.storageContext.Entry(category).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the category specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the category to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the category.
    /// </summary>
    /// <param name="category">The category to delete.</param>
    public void Delete(Category category)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          CREATE TEMP TABLE ""TempCategories"" (""Id"" INT PRIMARY KEY);
          WITH RECURSIVE ""X"" AS (
            SELECT ""Id"" FROM ""Categories"" WHERE ""CategoryId"" = {0}
            UNION ALL
            SELECT ""Categories"".""Id"" FROM ""Categories"" INNER JOIN ""X"" ON ""Categories"".""CategoryId"" = ""X"".""Id""
          )
          INSERT INTO ""TempCategories"" SELECT ""Id"" FROM ""X"";
          CREATE TEMP TABLE ""TempDictionaries"" (""Id"" INT PRIMARY KEY);
          INSERT INTO ""TempDictionaries"" VALUES ({1});
          INSERT INTO ""TempDictionaries"" SELECT ""NameId"" FROM ""Categories"" WHERE ""Id"" IN (SELECT ""Id"" FROM ""TempCategories"");
          DELETE FROM ""Categories"" WHERE ""Id"" IN (SELECT ""Id"" FROM ""TempCategories"");
          DELETE FROM ""Categories"" WHERE ""Id"" = {0};
          DELETE FROM ""Localizations"" WHERE ""DictionaryId"" IN (SELECT ""Id"" FROM ""TempDictionaries"");
          DELETE FROM ""Dictionaries"" WHERE ""Id"" IN (SELECT ""Id"" FROM ""TempDictionaries"");
        ",
        category.Id,
        category.NameId
      );
    }
  }
}