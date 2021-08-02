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

namespace Platformus.Core.Backend.ViewModels.Users
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(HttpContext httpContext, UserFilter filter, IEnumerable<User> users, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();

      return new IndexViewModel()
      {
        Grid = GridViewModelFactory.Create(
          httpContext,
          new[] {
            FilterViewModelFactory.Create(httpContext, "Name.Contains", localizer["Name"]),
            FilterViewModelFactory.Create(httpContext, "Credential.Identifier.Contains", localizer["Credential identifier"])
          },
          orderBy, skip, take, total,
          new[] {
            GridColumnViewModelFactory.Create(localizer["Name"], "Name"),
            GridColumnViewModelFactory.Create(localizer["Credentials"]),
            GridColumnViewModelFactory.Create(localizer["Created"], "Created"),
            GridColumnViewModelFactory.CreateEmpty()
          },
          users.Select(UserViewModelFactory.Create),
          "_User"
        )
      };
    }
  }
}