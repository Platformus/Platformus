// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.EntityFrameworkCore;
using Platformus.Navigation.Data.Models;

namespace Platformus.Navigation.Data.EntityFramework.Sqlite
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<CachedMenu>(etb =>
        {
          etb.HasKey(e => new { e.CultureId, e.MenuId });
          etb.ForSqliteToTable("CachedMenus");
        }
      );

      modelBuilder.Entity<Menu>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Menus");
        }
      );

      modelBuilder.Entity<MenuItem>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("MenuItems");
        }
      );
    }
  }
}