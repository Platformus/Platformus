// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Endpoints
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(HttpContext httpContext, EndpointFilter filter, IEnumerable<Data.Entities.Endpoint> endpoints, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();

      return new IndexViewModel()
      {
        Grid = GridViewModelFactory.Create(
          httpContext,
          new[] {
            FilterViewModelFactory.Create(httpContext, "Name.Contains", localizer["Name"]),
            FilterViewModelFactory.Create(httpContext, "UrlTemplate.Contains", localizer["URL template"])
          },
          orderBy, skip, take, total,
          new[] {
            GridColumnViewModelFactory.Create(localizer["Name"], "Name"),
            GridColumnViewModelFactory.Create(localizer["URL template"], "UrlTemplate"),
            GridColumnViewModelFactory.Create(localizer["Position"], "Position"),
            GridColumnViewModelFactory.Create(localizer["Data sources"]),
            GridColumnViewModelFactory.CreateEmpty()
          },
          endpoints.Select(EndpointViewModelFactory.Create),
          "_Endpoint"
        )
      };
    }
  }
}