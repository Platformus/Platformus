// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Data.EntityFramework.Sqlite
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Menu>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.ToTable("Menus");
        }
      );

      modelBuilder.Entity<MenuItem>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Url).HasMaxLength(128);
          etb.ToTable("MenuItems");
        }
      );

      modelBuilder.Entity<SerializedMenu>(etb =>
        {
          etb.HasKey(e => new { e.CultureId, e.MenuId });
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.ToTable("SerializedMenus");
        }
      );
    }
  }
}