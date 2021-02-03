// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.OrderStates
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(HttpContext httpContext, OrderStateFilter filter, IEnumerable<OrderState> orderStates, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.RequestServices.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory().Create(
          httpContext, "Name.Value.Contains", orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["Name"], httpContext.CreateLocalizedOrderBy("Name")),
            new GridColumnViewModelFactory().Create(localizer["Position"], "Position"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          orderStates.Select(os => new OrderStateViewModelFactory().Create(os)),
          "_OrderState"
        )
      };
    }
  }
}