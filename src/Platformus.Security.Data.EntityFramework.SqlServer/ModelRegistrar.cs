// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Data.EntityFramework.SqlServer
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Permission>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Permissions");
        }
      );

      modelBuilder.Entity<Role>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Roles");
        }
      );

      modelBuilder.Entity<RolePermission>(etb =>
        {
          etb.HasKey(e => new { e.RoleId, e.PermissionId });
          etb.ForSqlServerToTable("RolePermissions");
        }
      );

      modelBuilder.Entity<User>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Users");
        }
      );

      modelBuilder.Entity<UserRole>(etb =>
        {
          etb.HasKey(e => new { e.UserId, e.RoleId });
          etb.ForSqlServerToTable("UserRoles");
        }
      );

      modelBuilder.Entity<CredentialType>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("CredentialTypes");
        }
      );

      modelBuilder.Entity<Credential>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).UseSqlServerIdentityColumn();
          etb.ForSqlServerToTable("Credentials");
        }
      );
    }
  }
}