// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
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
    /// <summary>
    /// Gets the permission by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the permission.</param>
    /// <returns>Found permission with the given identifier.</returns>
    public Permission WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the permission by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the permission.</param>
    /// <returns>Found permission with the given code.</returns>
    public Permission WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(p => string.Equals(p.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the permissions using sorting by position (ascending).
    /// </summary>
    /// <returns>Found permissions.</returns>
    public IEnumerable<Permission> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(p => p.Position);
    }

    /// <summary>
    /// Gets all the permissions using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The permission property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of permissions that should be skipped.</param>
    /// <param name="take">The number of permissions that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found permissions using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Permission> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredPermissions(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the permission.
    /// </summary>
    /// <param name="permission">The permission to create.</param>
    public void Create(Permission permission)
    {
      this.dbSet.Add(permission);
    }

    /// <summary>
    /// Edits the permission.
    /// </summary>
    /// <param name="permission">The permission to edit.</param>
    public void Edit(Permission permission)
    {
      this.storageContext.Entry(permission).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the permission specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the permission to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the permission.
    /// </summary>
    /// <param name="permission">The permission to delete.</param>
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

    /// <summary>
    /// Counts the number of the permissions with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of permissions found.</returns>
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