// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Backend.ViewModels.Shared;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.ViewModels.Roles
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          RolePermissions = this.GetRolePermissions()
        };

      Role role = this.RequestHandler.Storage.GetRepository<IRoleRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = role.Id,
        Code = role.Code,
        Name = role.Name,
        Position = role.Position,
        RolePermissions = this.GetRolePermissions(role)
      };
    }

    public IEnumerable<RolePermissionViewModel> GetRolePermissions(Role role = null)
    {
      return this.RequestHandler.Storage.GetRepository<IPermissionRepository>().All().Select(
        p => new RolePermissionViewModelFactory(this.RequestHandler).Create(role, p)
      );
    }
  }
}