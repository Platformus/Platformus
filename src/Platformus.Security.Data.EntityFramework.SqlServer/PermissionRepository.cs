// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IPermissionRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Permission"/> entities in the context of SQL Server database.
  /// </summary>
  public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
  {
    public Permission WithKey(int id)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Permission> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(p => p.Position);
    }

    public IEnumerable<Permission> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredPermissions(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public void Create(Permission permission)
    {
      this.dbSet.Add(permission);
    }

    public void Edit(Permission permission)
    {
      this.storageContext.Entry(permission).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Permission permission)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM RolePermissions WHERE PermissionId = {0};
        ",
        permission.Id
      );

      this.dbSet.Remove(permission);
    }

    public int Count(string filter)
    {
      return this.GetFilteredPermissions(dbSet, filter).Count();
    }

    private IQueryable<Permission> GetFilteredPermissions(IQueryable<Permission> permissions, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return permissions;

      return permissions.Where(p => p.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}