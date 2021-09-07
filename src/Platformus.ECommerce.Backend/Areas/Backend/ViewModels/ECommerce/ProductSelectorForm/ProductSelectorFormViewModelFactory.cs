// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.ECommerce
{
  public static class ProductSelectorFormViewModelFactory
  {
    public static ProductSelectorFormViewModel Create(HttpContext httpContext, IEnumerable<Product> products, int? productId)
    {
      IStringLocalizer<ProductSelectorFormViewModel> localizer = httpContext.GetStringLocalizer<ProductSelectorFormViewModel>();

      return new ProductSelectorFormViewModel()
      {
        TableColumns = new[] {
          new TableTagHelper.Column(localizer["Category"]),
          new TableTagHelper.Column(localizer["Name"])
        },
        Products = products.Select(ProductViewModelFactory.Create),
        ProductId = productId
      };
    }
  }
}