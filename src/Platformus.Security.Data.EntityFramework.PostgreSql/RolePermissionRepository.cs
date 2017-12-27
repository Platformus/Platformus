// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.EntityFramework.PostgreSql
{
  /// <summary>
  /// Implements the <see cref="IRolePermissionRepository"/> interface and represents the repository
  /// for manipulating the <see cref="RolePermission"/> entities in the context of PostgreSQL database.
  /// </summary>
  public class RolePermissionRepository : RepositoryBase<RolePermission>, IRolePermissionRepository
  {
    /// <summary>
    /// Gets the role permission by the role identifier and permission identifier.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role this role permission is related to.</param>
    /// <param name="permissionId">The unique identifier of the permission this role permission is related to.</param>
    /// <returns>Found role permission with the given role identifier and permission identifier.</returns>
    public RolePermission WithKey(int roleId, int permissionId)
    {
      return this.dbSet.Find(roleId, permissionId);
    }

    /// <summary>
    /// Gets the role permissions filtered by the role identifier.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role these role permissions belongs to.</param>
    public IEnumerable<RolePermission> FilteredByRoleId(int roleId)
    {
      return this.dbSet.AsNoTracking().Where(rp => rp.RoleId == roleId);
    }

    /// <summary>
    /// Creates the role permission.
    /// </summary>
    /// <param name="rolePermission">The role permission to create.</param>
    public void Create(RolePermission rolePermission)
    {
      this.dbSet.Add(rolePermission);
    }

    /// <summary>
    /// Edits the role permission.
    /// </summary>
    /// <param name="rolePermission">The role permission to edit.</param>
    public void Edit(RolePermission rolePermission)
    {
      this.storageContext.Entry(rolePermission).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the role permission specified by the role identifier and permission identifier.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role this role permission is related to.</param>
    /// <param name="permissionId">The unique identifier of the permission this role permission is related to.</param>
    public void Delete(int roleId, int permissionId)
    {
      this.Delete(this.WithKey(roleId, permissionId));
    }

    /// <summary>
    /// Deletes the role permission.
    /// </summary>
    /// <param name="rolePermission">The role permission to delete.</param>
    public void Delete(RolePermission rolePermission)
    {
      this.dbSet.Remove(rolePermission);
    }
  }
}