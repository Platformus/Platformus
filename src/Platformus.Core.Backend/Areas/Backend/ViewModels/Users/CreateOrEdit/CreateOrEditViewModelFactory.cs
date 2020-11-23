// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Data.Entities;
using Platformus.Core.Extensions;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.ViewModels.Users
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, User user)
    {
      if (user == null)
        return new CreateOrEditViewModel()
        {
          UserRoles = await this.GetUserRolesAsync(httpContext)
        };

      return new CreateOrEditViewModel()
      {
        Id = user.Id,
        Name = user.Name,
        UserRoles = await this.GetUserRolesAsync(httpContext, user)
      };
    }

    public async Task<IEnumerable<UserRoleViewModel>> GetUserRolesAsync(HttpContext httpContext, User user = null)
    {
      return (await httpContext.GetStorage().GetRepository<int, Role, RoleFilter>().GetAllAsync()).Select(
        r => new UserRoleViewModelFactory().Create(r, user != null && user.UserRoles.Any(ur => ur.RoleId == r.Id))
      );
    }
  }
}