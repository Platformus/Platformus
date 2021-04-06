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
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(HttpContext httpContext, UserFilter filter, IEnumerable<User> users, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory().Create(
          httpContext,
          new[] {
            new FilterViewModelFactory().Create(httpContext, "Name.Contains", localizer["Name"]),
            new FilterViewModelFactory().Create(httpContext, "Credential.Identifier.Contains", localizer["Credential identifier"])
          },
          orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory().Create(localizer["Credentials"]),
            new GridColumnViewModelFactory().Create(localizer["Created"], "Created"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          users.Select(u => new UserViewModelFactory().Create(u)),
          "_User"
        )
      };
    }
  }
}