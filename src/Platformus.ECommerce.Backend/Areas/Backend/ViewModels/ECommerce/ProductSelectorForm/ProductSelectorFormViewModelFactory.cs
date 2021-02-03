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

namespace Platformus.ECommerce.Backend.ViewModels.ECommerce
{
  public class ProductSelectorFormViewModelFactory : ViewModelFactoryBase
  {
    public ProductSelectorFormViewModel Create(HttpContext httpContext, IEnumerable<Product> products, int? productId)
    {
      IStringLocalizer<ProductSelectorFormViewModelFactory> localizer = httpContext.RequestServices.GetService<IStringLocalizer<ProductSelectorFormViewModelFactory>>();

      return new ProductSelectorFormViewModel()
      {
        GridColumns = new[] {
          new GridColumnViewModelFactory().Create(localizer["Category"]),
          new GridColumnViewModelFactory().Create(localizer["Name"])
        },
        Products = products.Select(p => new ProductViewModelFactory().Create(p)),
        ProductId = productId
      };
    }
  }
}