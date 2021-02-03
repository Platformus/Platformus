// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Core;
using Platformus.Core.Frontend.ViewModels;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Frontend.ViewModels.Shared;
using Platformus.ECommerce.ProductProviders;

namespace Platformus.ECommerce.Frontend.ViewModels.ECommerce
{
  public class CatalogPageViewModelFactory : ViewModelFactoryBase
  {
    public async Task<CatalogPageViewModel> CreateAsync(HttpContext httpContext, Catalog catalog)
    {
      return new CatalogPageViewModel()
      {
        Name = catalog.Name.GetLocalizationValue(),
        Products = await this.GetProductsAsync(httpContext, catalog)
      };
    }

    private async Task<IEnumerable<ProductViewModel>> GetProductsAsync(HttpContext httpContext, Catalog catalog)
    {
      IProductProvider productProvider = StringActivator.CreateInstance<IProductProvider>(catalog.CSharpClassName);

      return (await productProvider.GetProductsAsync(httpContext, catalog)).Select(
        p => new ProductViewModelFactory().Create(p)
      );
    }
  }
}