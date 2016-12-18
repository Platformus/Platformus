// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Data.EntityFramework.PostgreSql
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Permission>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id); //.UsePostgreSqlIdentityColumn();
          etb.ForNpgsqlToTable("Permissions");
        }
      );

      modelBuilder.Entity<Role>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id); //.UsePostgreSqlIdentityColumn();
          etb.ForNpgsqlToTable("Roles");
        }
      );

      modelBuilder.Entity<RolePermission>(etb =>
        {
          etb.HasKey(e => new { e.RoleId, e.PermissionId });
          etb.ForNpgsqlToTable("RolePermissions");
        }
      );

      modelBuilder.Entity<User>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id); //.UsePostgreSqlIdentityColumn();
          etb.ForNpgsqlToTable("Users");
        }
      );

      modelBuilder.Entity<UserRole>(etb =>
        {
          etb.HasKey(e => new { e.UserId, e.RoleId });
          etb.ForNpgsqlToTable("UserRoles");
        }
      );

      modelBuilder.Entity<CredentialType>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id); //.UsePostgreSqlIdentityColumn();
          etb.ForNpgsqlToTable("CredentialTypes");
        }
      );

      modelBuilder.Entity<Credential>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);//.UsePostgreSqlIdentityColumn();
          etb.ForNpgsqlToTable("Credentials");
        }
      );
    }
  }
}