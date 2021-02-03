// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Orders
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(HttpContext httpContext, OrderFilter filter, IEnumerable<Order> orders, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.RequestServices.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory().Create(
          httpContext, null, orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["#"]),
            new GridColumnViewModelFactory().Create(localizer["Order state"]),
            new GridColumnViewModelFactory().Create(localizer["Delivery method"]),
            new GridColumnViewModelFactory().Create(localizer["Payment method"]),
            new GridColumnViewModelFactory().Create(localizer["Customer"]),
            new GridColumnViewModelFactory().Create(localizer["Total"]),
            new GridColumnViewModelFactory().Create(localizer["Created"], "Created"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          orders.Select(o => new OrderViewModelFactory().Create(o)),
          "_Order"
        )
      };
    }
  }
}