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

namespace Platformus.ECommerce.Backend.ViewModels.PaymentMethods
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(HttpContext httpContext, PaymentMethodFilter filter, IEnumerable<PaymentMethod> paymentMethods, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();

      return new IndexViewModel()
      {
        Grid = GridViewModelFactory.Create(
          httpContext,
          FilterViewModelFactory.Create(httpContext, "Name.Value.Contains", localizer["Name"]),
          orderBy, skip, take, total,
          new[] {
            GridColumnViewModelFactory.Create(localizer["Name"], httpContext.CreateLocalizedOrderBy("Name")),
            GridColumnViewModelFactory.Create(localizer["Position"], "Position"),
            GridColumnViewModelFactory.CreateEmpty()
          },
          paymentMethods.Select(PaymentMethodViewModelFactory.Create),
          "_PaymentMethod"
        )
      };
    }
  }
}