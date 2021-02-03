// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.CompletedForms
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(HttpContext httpContext, CompletedFormFilter filter, IEnumerable<CompletedForm> completedForms, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.RequestServices.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Filter = filter,
        Grid = new GridViewModelFactory().Create(
          httpContext, "Form.Name.Contains", orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["Form"]),
            new GridColumnViewModelFactory().Create(localizer["Created"], "created"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          completedForms.Select(cf => new CompletedFormViewModelFactory().Create(cf)),
          "_CompletedForm"
        )
      };
    }
  }
}