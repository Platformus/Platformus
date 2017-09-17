// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="RolePermission"/> entities.
  /// </summary>
  public interface IRolePermissionRepository : IRepository
  {
    /// <summary>
    /// Gets the role permission by the role identifier and permission identifier.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role this role permission is related to.</param>
    /// <param name="permissionId">The unique identifier of the permission this role permission is related to.</param>
    /// <returns>Found role permission with the given role identifier and permission identifier.</returns>
    RolePermission WithKey(int roleId, int permissionId);

    /// <summary>
    /// Gets the role permissions filtered by the role identifier.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role these role permissions belongs to.</param>
    IEnumerable<RolePermission> FilteredByRoleId(int roleId);

    /// <summary>
    /// Creates the role permission.
    /// </summary>
    /// <param name="rolePermission">The role permission to create.</param>
    void Create(RolePermission rolePermission);

    /// <summary>
    /// Edits the role permission.
    /// </summary>
    /// <param name="rolePermission">The role permission to edit.</param>
    void Edit(RolePermission rolePermission);

    /// <summary>
    /// Deletes the role permission specified by the role identifier and permission identifier.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role this role permission is related to.</param>
    /// <param name="permissionId">The unique identifier of the permission this role permission is related to.</param>
    void Delete(int roleId, int permissionId);

    /// <summary>
    /// Deletes the role permission.
    /// </summary>
    /// <param name="rolePermission">The role permission to delete.</param>
    void Delete(RolePermission rolePermission);
  }
}