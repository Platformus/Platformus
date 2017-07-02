// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.ViewModels.Shared
{
  public class RolePermissionViewModelFactory : ViewModelFactoryBase
  {
    public RolePermissionViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public RolePermissionViewModel Create(Role role, Permission permission)
    {
      RolePermission rolePermission = null;

      if (role != null)
        rolePermission = this.RequestHandler.Storage.GetRepository<IRolePermissionRepository>().WithKey(role.Id, permission.Id);

      return new RolePermissionViewModel()
      {
        Permission = new PermissionViewModelFactory(this.RequestHandler).Create(permission),
        IsAssigned = rolePermission != null
      };
    }
  }
}