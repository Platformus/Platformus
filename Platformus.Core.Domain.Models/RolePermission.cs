// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Core.Domain.Models;

public class RolePermission : IModel<Data.Entities.RolePermission, RolePermissionFilter>
{
  public Role? Role { get; set; }
  public Permission? Permission { get; set; }

  public RolePermission() { }

  public RolePermission(Data.Entities.RolePermission _rolePermission) : this(_rolePermission, mapRole: true, mapPermission: true) { }

  public RolePermission(Data.Entities.RolePermission _rolePermission, bool mapRole = true, bool mapPermission = true)
  {
    this.Role = mapRole ? _rolePermission.Role == null ? new Role { Id = _rolePermission.RoleId } : new Role(_rolePermission.Role, mapRolePermissions: false) : null;
    this.Permission = _rolePermission.Permission == null ? new Permission { Id = _rolePermission.PermissionId } : new Permission(_rolePermission.Permission, mapRolePermissions: false);
  }

  public Data.Entities.RolePermission ToEntity()
  {
    return new Data.Entities.RolePermission()
    {
      RoleId = this.Role?.Id ?? 0,
      PermissionId = this.Permission?.Id ?? 0
    };
  }
}