// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Endpoints
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(HttpContext httpContext, EndpointFilter filter, IEnumerable<Data.Entities.Endpoint> endpoints, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory().Create(
          httpContext,
          new[] {
            new FilterViewModelFactory().Create(httpContext, "Name.Contains", localizer["Name"]),
            new FilterViewModelFactory().Create(httpContext, "UrlTemplate.Contains", localizer["URL template"])
          },
          orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory().Create(localizer["URL template"], "UrlTemplate"),
            new GridColumnViewModelFactory().Create(localizer["Position"], "Position"),
            new GridColumnViewModelFactory().Create(localizer["Data sources"]),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          endpoints.Select(e => new EndpointViewModelFactory().Create(e)),
          "_Endpoint"
        )
      };
    }
  }
}