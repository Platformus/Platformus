// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.ViewModels.Roles
{
  public static class CreateOrEditViewModelFactory
  {
    public static async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, Role role)
    {
      if (role == null)
        return new CreateOrEditViewModel()
        {
          RolePermissions = await GetRolePermissionsAsync(httpContext)
        };

      return new CreateOrEditViewModel()
      {
        Id = role.Id,
        Code = role.Code,
        Name = role.Name,
        Position = role.Position,
        RolePermissions = await GetRolePermissionsAsync(httpContext, role)
      };
    }

    public static async Task<IEnumerable<RolePermissionViewModel>> GetRolePermissionsAsync(HttpContext httpContext, Role role = null)
    {
      return (await httpContext.GetStorage().GetRepository<int, Permission, PermissionFilter>().GetAllAsync()).Select(
        p => RolePermissionViewModelFactory.Create(p, role != null && role.RolePermissions.Any(rp => rp.PermissionId == p.Id))
      );
    }
  }
}