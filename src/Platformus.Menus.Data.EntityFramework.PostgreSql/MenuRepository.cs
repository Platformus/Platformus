// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Data.EntityFramework.PostgreSql
{
  /// <summary>
  /// Implements the <see cref="IMenuRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Menu"/> entities in the context of PostgreSQL database.
  /// </summary>
  public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
  {
    /// <summary>
    /// Gets the menu by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the menu.</param>
    /// <returns>Found menu with the given identifier.</returns>
    public Menu WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the menu by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the menu.</param>
    /// <returns>Found menu with the given code.</returns>
    public Menu WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(m => string.Equals(m.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the menus using sorting by code (ascending).
    /// </summary>
    /// <returns>Found menus.</returns>
    public IEnumerable<Menu> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(m => m.Code);
    }

    /// <summary>
    /// Creates the menu.
    /// </summary>
    /// <param name="menu">The menu to create.</param>
    public void Create(Menu menu)
    {
      this.dbSet.Add(menu);
    }

    /// <summary>
    /// Edits the menu.
    /// </summary>
    /// <param name="menu">The menu to edit.</param>
    public void Edit(Menu menu)
    {
      this.storageContext.Entry(menu).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the menu specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the menu to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the menu.
    /// </summary>
    /// <param name="menu">The menu to delete.</param>
    public void Delete(Menu menu)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM ""SerializedMenus"" WHERE ""MenuId"" = {0};
          CREATE TEMP TABLE ""TempMenuItems"" (""Id"" INT PRIMARY KEY);
          WITH RECURSIVE ""X"" AS (
            SELECT ""Id"" FROM ""MenuItems"" WHERE ""MenuId"" = {0}
            UNION ALL
            SELECT ""MenuItems"".""Id"" FROM ""MenuItems"" INNER JOIN ""X"" ON ""MenuItems"".""MenuItemId"" = ""X"".""Id""
          )
          INSERT INTO ""TempMenuItems"" SELECT ""Id"" FROM ""X"";
          CREATE TEMP TABLE ""TempDictionaries"" (""Id"" INT PRIMARY KEY);
          INSERT INTO ""TempDictionaries"" VALUES ({1});
          INSERT INTO ""TempDictionaries"" SELECT ""NameId"" FROM ""MenuItems"" WHERE ""Id"" IN (SELECT ""Id"" FROM ""TempMenuItems"");
          DELETE FROM ""MenuItems"" WHERE ""Id"" IN (SELECT ""Id"" FROM ""TempMenuItems"");
          DELETE FROM ""Menus"" WHERE ""Id"" = {0};
          DELETE FROM ""Localizations"" WHERE ""DictionaryId"" IN (SELECT ""Id"" FROM ""TempDictionaries"");
          DELETE FROM ""Dictionaries"" WHERE ""Id"" IN (SELECT ""Id"" FROM ""TempDictionaries"");
        ",
        menu.Id,
        menu.NameId
      );
    }
  }
}