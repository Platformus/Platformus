// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.EntityFrameworkCore;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Data.EntityFramework.Sqlite
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Permission>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Permissions");
        }
      );

      modelBuilder.Entity<Role>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Roles");
        }
      );

      modelBuilder.Entity<RolePermission>(etb =>
        {
          etb.HasKey(e => new { e.RoleId, e.PermissionId });
          etb.ForSqliteToTable("RolePermissions");
        }
      );

      modelBuilder.Entity<User>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Users");
        }
      );

      modelBuilder.Entity<UserRole>(etb =>
        {
          etb.HasKey(e => new { e.UserId, e.RoleId });
          etb.ForSqliteToTable("UserRoles");
        }
      );

      modelBuilder.Entity<CredentialType>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("CredentialTypes");
        }
      );

      modelBuilder.Entity<Credential>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);// .UseSqlServerIdentityColumn();
          etb.ForSqliteToTable("Credentials");
        }
      );
    }
  }
}