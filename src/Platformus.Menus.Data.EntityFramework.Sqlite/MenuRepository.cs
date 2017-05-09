// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.EntityFrameworkCore;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Models;

namespace Platformus.Menus.Data.EntityFramework.Sqlite
{
  public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
  {
    public Menu WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(m => m.Id == id);
    }

    public Menu WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(m => string.Equals(m.Code, code, System.StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Menu> All()
    {
      return this.dbSet.OrderBy(m => m.Code);
    }

    public void Create(Menu menu)
    {
      this.dbSet.Add(menu);
    }

    public void Edit(Menu menu)
    {
      this.storageContext.Entry(menu).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Menu menu)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM SerializedMenus WHERE MenuId = {0};
          CREATE TEMP TABLE TempMenuItems (Id INT PRIMARY KEY);
          WITH X AS (
            SELECT Id FROM MenuItems WHERE MenuId = {0}
            UNION ALL
            SELECT MenuItems.Id FROM MenuItems INNER JOIN X ON MenuItems.MenuItemId = X.Id
          )
          INSERT INTO TempMenuItems SELECT Id FROM X;
          CREATE TEMP TABLE TempDictionaries (Id INT PRIMARY KEY);
          INSERT INTO TempDictionaries VALUES ({1});
          INSERT INTO TempDictionaries SELECT NameId FROM MenuItems WHERE Id IN (SELECT Id FROM TempMenuItems);
          DELETE FROM MenuItems WHERE Id IN (SELECT Id FROM TempMenuItems);
          DELETE FROM Menus WHERE Id = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM TempDictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM TempDictionaries);
        ",
        menu.Id,
        menu.NameId
      );
    }
  }
}