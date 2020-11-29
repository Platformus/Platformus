// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.SqlServer
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Catalog>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Url).IsRequired().HasMaxLength(128);
          etb.ToTable("Catalogs");
        }
      );

      modelBuilder.Entity<Category>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.ToTable("Categories");
        }
      );

      modelBuilder.Entity<Product>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Url).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.ToTable("Products");
        }
      );

      modelBuilder.Entity<Photo>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Filename).IsRequired().HasMaxLength(64);
          etb.ToTable("Photos");
        }
      );
    }
  }
}