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
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(HttpContext httpContext, CredentialFilter filter, IEnumerable<Credential> credentials, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();

      return new IndexViewModel()
      {
        Filter = filter,
        Grid = new GridViewModelFactory().Create(
          httpContext,
          new FilterViewModelFactory().Create(httpContext, "Identifier.Contains", localizer["Identifier"]),
          orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["Credential Type"]),
            new GridColumnViewModelFactory().Create(localizer["Identifier"], "Identifier"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          credentials.Select(c => new CredentialViewModelFactory().Create(c)),
          "_Credential"
        )
      };
    }
  }
}