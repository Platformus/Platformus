// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Data.Entities;
using Platformus.Core.Extensions;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.ViewModels.Permissions
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(HttpContext httpContext, PermissionFilter filter, IEnumerable<Permission> permissions, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory().Create(
          httpContext,
          new FilterViewModelFactory().Create(httpContext, "Name.Contains", localizer["Name"]),
          orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory().Create(localizer["Position"], "Position"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          permissions.Select(p => new PermissionViewModelFactory().Create(p)),
          "_Permission"
        )
      };
    }
  }
}