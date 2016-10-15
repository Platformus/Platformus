// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Backend.ViewModels.Shared
{
  public class RolePermissionViewModelFactory : ViewModelFactoryBase
  {
    public RolePermissionViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public RolePermissionViewModel Create(Role role, Permission permission)
    {
      RolePermission rolePermission = null;

      if (role != null)
        rolePermission = this.handler.Storage.GetRepository<IRolePermissionRepository>().WithKey(role.Id, permission.Id);

      return new RolePermissionViewModel()
      {
        Permission = new PermissionViewModelFactory(this.handler).Create(permission),
        IsAssigned = rolePermission != null
      };
    }
  }
}