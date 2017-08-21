// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Data.EntityFramework.SqlServer
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
	    modelBuilder.Entity<Endpoint>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.Property(e => e.UrlTemplate).HasMaxLength(128);
          etb.Property(e => e.SignInUrl).HasMaxLength(128);
          etb.Property(e => e.CSharpClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Parameters).HasMaxLength(1024);
          etb.ToTable("Endpoints");
        }
      );

      modelBuilder.Entity<EndpointPermission>(etb =>
        {
          etb.HasKey(e => new { e.EndpointId, e.PermissionId });
          etb.ToTable("EndpointPermissions");
        }
      );

      modelBuilder.Entity<DataSource>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.CSharpClassName).IsRequired().HasMaxLength(128);
          etb.Property(e => e.Parameters).HasMaxLength(1024);
          etb.ToTable("DataSources");
        }
      );
    }
  }
}