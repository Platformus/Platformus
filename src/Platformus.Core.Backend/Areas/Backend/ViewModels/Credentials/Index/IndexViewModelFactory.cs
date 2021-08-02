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

namespace Platformus.Core.Backend.ViewModels.Credentials
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(HttpContext httpContext, CredentialFilter filter, IEnumerable<Credential> credentials, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();

      return new IndexViewModel()
      {
        Filter = filter,
        Grid = GridViewModelFactory.Create(
          httpContext,
          FilterViewModelFactory.Create(httpContext, "Identifier.Contains", localizer["Identifier"]),
          orderBy, skip, take, total,
          new[] {
            GridColumnViewModelFactory.Create(localizer["Credential Type"]),
            GridColumnViewModelFactory.Create(localizer["Identifier"], "Identifier"),
            GridColumnViewModelFactory.CreateEmpty()
          },
          credentials.Select(CredentialViewModelFactory.Create),
          "_Credential"
        )
      };
    }
  }
}