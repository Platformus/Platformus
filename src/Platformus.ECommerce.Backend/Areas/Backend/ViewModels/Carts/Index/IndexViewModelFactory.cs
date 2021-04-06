// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Carts
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(HttpContext httpContext, CartFilter filter, IEnumerable<Cart> carts, string cartBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory().Create(
          httpContext, cartBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["#"]),
            new GridColumnViewModelFactory().Create(localizer["Total"]),
            new GridColumnViewModelFactory().Create(localizer["Created"], "Created"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          carts.Select(c => new CartViewModelFactory().Create(c)),
          "_Cart"
        )
      };
    }
  }
}