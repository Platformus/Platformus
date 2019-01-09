// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.ECommerce
{
  public class ProductSelectorFormViewModelFactory : ViewModelFactoryBase
  {
    public ProductSelectorFormViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ProductSelectorFormViewModel Create(int? productId)
    {
      IStringLocalizer<ProductSelectorFormViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<ProductSelectorFormViewModelFactory>>();

      return new ProductSelectorFormViewModel()
      {
        GridColumns = new[] {
          new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Category"]),
          new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Name"])
        },
        Products = this.RequestHandler.Storage.GetRepository<IProductRepository>().All().ToList().Select(
          p => new ProductViewModelFactory(this.RequestHandler).Create(p)
        ),
        ProductId = productId
      };
    }
  }
}