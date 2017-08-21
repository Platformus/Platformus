// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Data.EntityFramework.SqlServer
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Culture>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ToTable("Cultures");
        }
      );

      modelBuilder.Entity<Dictionary>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ToTable("Dictionaries");
        }
      );

      modelBuilder.Entity<Localization>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ToTable("Localizations");
        }
      );
    }
  }
}