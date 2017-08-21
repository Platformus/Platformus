// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Configurations.Data.Entities;

namespace Platformus.Configurations.Data.EntityFramework.SqlServer
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Configuration>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ToTable("Configurations");
        }
      );

      modelBuilder.Entity<Variable>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.Property(e => e.Value).IsRequired().HasMaxLength(1024);
          etb.ToTable("Variables");
        }
      );
    }
  }
}