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
    RolePermission WithKey(int roleId, int permissionId);
    IEnumerable<RolePermission> FilteredByRoleId(int roleId);
    void Create(RolePermission rolePermission);
    void Edit(RolePermission rolePermission);
    void Delete(int roleId, int permissionId);
    void Delete(RolePermission rolePermission);
  }
}