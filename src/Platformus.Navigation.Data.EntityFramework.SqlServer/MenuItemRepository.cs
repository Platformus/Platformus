// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.Data.Entity;
using Platformus.Navigation.Data.Abstractions;
using Platformus.Navigation.Data.Models;

namespace Platformus.Navigation.Data.EntityFramework.SqlServer
{
  public class MenuItemRepository : RepositoryBase<MenuItem>, IMenuItemRepository
  {
    public MenuItem WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(mi => mi.Id == id);
    }

    public IEnumerable<MenuItem> FilteredByMenuId(int menuId)
    {
      return this.dbSet.Where(mi => mi.MenuId == menuId).OrderBy(mi => mi.Position);
    }

    public IEnumerable<MenuItem> FilteredByMenuItemId(int menuItemId)
    {
      return this.dbSet.Where(mi => mi.MenuItemId == menuItemId).OrderBy(mi => mi.Position);
    }

    public void Create(MenuItem menuItem)
    {
      this.dbSet.Add(menuItem);
    }

    public void Edit(MenuItem menuItem)
    {
      this.dbContext.Entry(menuItem).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(MenuItem menuItem)
    {
      this.dbContext.Database.ExecuteSqlCommand(
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