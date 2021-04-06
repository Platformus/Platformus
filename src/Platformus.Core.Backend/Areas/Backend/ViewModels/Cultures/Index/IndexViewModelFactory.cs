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

namespace Platformus.Core.Backend.ViewModels.Cultures
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(HttpContext httpContext, CultureFilter filter, IEnumerable<Culture> cultures, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory().Create(
          httpContext,
          new[] {
            new FilterViewModelFactory().Create(httpContext, "Id", localizer["Two letter country code (ISO 3166)"]),
            new FilterViewModelFactory().Create(httpContext, "Name.Contains", localizer["Name"])
          },
          orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["Two letter country code (ISO 3166)"], "Id"),
            new GridColumnViewModelFactory().Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          cultures.Select(c => new CultureViewModelFactory().Create(c)),
          "_Culture"
        )
      };
    }
  }
}