// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IMenuItemRepository"/> interface and represents the repository
  /// for manipulating the <see cref="MenuItem"/> entities in the context of SQL Server database.
  /// </summary>
  public class MenuItemRepository : RepositoryBase<MenuItem>, IMenuItemRepository
  {
    /// <summary>
    /// Gets the menu item by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the menu item.</param>
    /// <returns>Found menu item with the given identifier.</returns>
    public MenuItem WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the menu items filtered by the menu identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="menuId">The unique identifier of the menu these menu items belongs to.</param>
    /// <returns>Found menu items.</returns>
    public IEnumerable<MenuItem> FilteredByMenuId(int menuId)
    {
      return this.dbSet.AsNoTracking().Where(mi => mi.MenuId == menuId).OrderBy(mi => mi.Position);
    }

    /// <summary>
    /// Gets the menu items filtered by the parent menu item identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="menuItemId">The unique identifier of the parent menu item these menu items belongs to.</param>
    /// <returns>Found menu items.</returns>
    public IEnumerable<MenuItem> FilteredByMenuItemId(int menuItemId)
    {
      return this.dbSet.AsNoTracking().Where(mi => mi.MenuItemId == menuItemId).OrderBy(mi => mi.Position);
    }

    /// <summary>
    /// Creates the menu item.
    /// </summary>
    /// <param name="menuItem">The menu item to create.</param>
    public void Create(MenuItem menuItem)
    {
      this.dbSet.Add(menuItem);
    }

    /// <summary>
    /// Edits the menu item.
    /// </summary>
    /// <param name="menuItem">The menu item to edit.</param>
    public void Edit(MenuItem menuItem)
    {
      this.storageContext.Entry(menuItem).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the menu item specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the menu item to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the menu item.
    /// </summary>
    /// <param name="menuItem">The menu item to delete.</param>
    public void Delete(MenuItem menuItem)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          CREATE TABLE #MenuItems (Id INT PRIMARY KEY);
          WITH X AS (
            SELECT Id FROM MenuItems WHERE Id = {0}
            UNION ALL
            SELECT MenuItems.Id FROM MenuItems INNER JOIN X ON MenuItems.MenuItemId = X.Id
          )
          INSERT INTO #MenuItems SELECT Id FROM X;
          CREATE TABLE #Dictionaries (Id INT PRIMARY KEY);
          INSERT INTO #Dictionaries SELECT NameId FROM MenuItems WHERE Id IN (SELECT Id FROM #MenuItems);
          DELETE FROM MenuItems WHERE Id IN (SELECT Id FROM #MenuItems);
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM #Dictionaries);
        ",
        menuItem.Id
      );
    }
  }
}