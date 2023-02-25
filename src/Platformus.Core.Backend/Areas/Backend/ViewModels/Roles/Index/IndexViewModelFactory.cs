// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend.ViewModels.Roles;

public static class IndexViewModelFactory
{
  public static async Task<IndexViewModel> CreateAsync(HttpContext httpContext, string sorting, int offset, int limit, int total, IEnumerable<Role> roles)
  {
    return new IndexViewModel()
    {
      PermissionOptions = await GetPermissionOptionsAsync(httpContext),
      Sorting = sorting,
      Offset = offset,
      Limit = limit,
      Total = total,
      Roles = roles.Select(RoleViewModelFactory.Create).ToList()
    };
  }

  private static async Task<IEnumerable<Option>> GetPermissionOptionsAsync(HttpContext httpContext)
  {
    IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();
    List<Option> options = new List<Option>();

    options.Add(new Option(localizer["All permissions"], string.Empty));
    options.AddRange(
      (await httpContext.GetStorage().GetRepository<int, Permission, PermissionFilter>().GetAllAsync(sorting: "+position")).Select(
        p => new Option(p.Name, p.Id.ToString())
      )
    );

    return options;
  }
}