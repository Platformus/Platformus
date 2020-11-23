// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Core.Data.Entities;

namespace Platformus.Core.Data.EntityFramework.SqlServer
{
  public class EntityRegistrar : IEntityRegistrar
  {
    public void RegisterEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseIdentityColumn();
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ToTable("Users");
        }
      );

	  modelBuilder.Entity<CredentialType>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseIdentityColumn();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ToTable("CredentialTypes");
        }
      );

      modelBuilder.Entity<Credential>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseIdentityColumn();
          etb.Property(e => e.Identifier).IsRequired().HasMaxLength(64);
          etb.Property(e => e.Secret).HasMaxLength(1024);
          etb.ToTable("Credentials");
        }
      );

	  modelBuilder.Entity<Role>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseIdentityColumn();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ToTable("Roles");
        }
      );

	  modelBuilder.Entity<UserRole>(etb =>
        {
          etb.HasKey(e => new { e.UserId, e.RoleId });
          etb.ToTable("UserRoles");
        }
      );

      modelBuilder.Entity<Permission>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseIdentityColumn();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ToTable("Permissions");
        }
      );

      modelBuilder.Entity<RolePermission>(etb =>
        {
          etb.HasKey(e => new { e.RoleId, e.PermissionId });
          etb.ToTable("RolePermissions");
        }
      );

      modelBuilder.Entity<Configuration>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseIdentityColumn();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ToTable("Configurations");
        }
      );

      modelBuilder.Entity<Variable>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseIdentityColumn();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.Property(e => e.Value).IsRequired().HasMaxLength(1024);
          etb.ToTable("Variables");
        }
      );

      modelBuilder.Entity<Culture>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseIdentityColumn();
          etb.Property(e => e.Code).IsRequired().HasMaxLength(32);
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ToTable("Cultures");
        }
      );

      modelBuilder.Entity<Dictionary>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseIdentityColumn();
          etb.ToTable("Dictionaries");
        }
      );

      modelBuilder.Entity<Localization>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseIdentityColumn();
          etb.ToTable("Localizations");
        }
      );
    }
  }
}