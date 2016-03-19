// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.EntityFramework.PostgreSql;
using Microsoft.Data.Entity;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Data.EntityFramework.PostgreSql
{
  public class ModelRegistrar : IModelRegistrar
  {
    public void RegisterModels(ModelBuilder modelbuilder)
    {
      modelbuilder.Entity<Permission>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id); //.UsePostgreSqlIdentityColumn();
          etb.ForNpgsqlToTable("Permissions");
        }
      );

      modelbuilder.Entity<Role>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id); //.UsePostgreSqlIdentityColumn();
          etb.ForNpgsqlToTable("Roles");
        }
      );

      modelbuilder.Entity<RolePermission>(etb =>
        {
          etb.HasKey(e => new { e.RoleId, e.PermissionId });
          etb.ForNpgsqlToTable("RolePermissions");
        }
      );

      modelbuilder.Entity<User>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id); //.UsePostgreSqlIdentityColumn();
          etb.ForNpgsqlToTable("Users");
        }
      );

      modelbuilder.Entity<UserRole>(etb =>
        {
          etb.HasKey(e => new { e.UserId, e.RoleId });
          etb.ForNpgsqlToTable("UserRoles");
        }
      );

      modelbuilder.Entity<CredentialType>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id); //.UsePostgreSqlIdentityColumn();
          etb.ForNpgsqlToTable("CredentialTypes");
        }
      );

      modelbuilder.Entity<Credential>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id);//.UsePostgreSqlIdentityColumn();
          etb.ForNpgsqlToTable("Credentials");
        }
      );
    }
  }
}