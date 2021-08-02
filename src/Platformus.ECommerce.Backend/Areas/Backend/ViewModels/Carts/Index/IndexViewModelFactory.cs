// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Carts
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(HttpContext httpContext, CartFilter filter, IEnumerable<Cart> carts, string cartBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();

      return new IndexViewModel()
      {
        Grid = GridViewModelFactory.Create(
          httpContext, cartBy, skip, take, total,
          new[] {
            GridColumnViewModelFactory.Create(localizer["#"]),
            GridColumnViewModelFactory.Create(localizer["Total"]),
            GridColumnViewModelFactory.Create(localizer["Created"], "Created"),
            GridColumnViewModelFactory.CreateEmpty()
          },
          carts.Select(CartViewModelFactory.Create),
          "_Cart"
        )
      };
    }
  }
}