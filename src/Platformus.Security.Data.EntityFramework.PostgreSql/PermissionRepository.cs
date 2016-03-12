// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.PostgreSql;
using Microsoft.Data.Entity;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Data.EntityFramework.PostgreSql
{
  public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
  {
    public Permission WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Permission> All()
    {
      return this.dbSet.OrderBy(p => p.Position);
    }

    public IEnumerable<Permission> Range(string orderBy, string direction, int skip, int take)
    {
      return this.dbSet.OrderBy(p => p.Position).Skip(skip).Take(take);
    }

    public void Create(Permission permission)
    {
      this.dbSet.Add(permission);
    }

    public void Edit(Permission permission)
    {
      this.dbContext.Entry(permission).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Permission permission)
    {
      this.dbContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM RolePermissions WHERE PermissionId = {0};
        ",
        permission.Id
      );

      this.dbSet.Remove(permission);
    }

    public int Count()
    {
      return this.dbSet.Count();
    }
  }
}