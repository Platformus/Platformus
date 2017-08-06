// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Data.EntityFramework.Sqlite
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Microcontroller>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.Property(e => e.UrlTemplate).HasMaxLength(128);
          etb.Property(e => e.SignInUrl).HasMaxLength(128);
          etb.Property(e => e.CSharpClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Parameters).HasMaxLength(1024);
          etb.ForSqliteToTable("Microcontrollers");
        }
      );

      modelBuilder.Entity<MicrocontrollerPermission>(etb =>
        {
          etb.HasKey(e => new { e.MicrocontrollerId, e.PermissionId });
          etb.ForSqliteToTable("MicrocontrollerPermissions");
        }
      );

      modelBuilder.Entity<DataSource>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.CSharpClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Parameters).HasMaxLength(1024);
          etb.ForSqliteToTable("DataSources");
        }
      );
    }
  }
}