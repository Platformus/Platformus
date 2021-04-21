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
  public class CategoryPageViewModelFactory : ViewModelFactoryBase
  {
    public async Task<CategoryPageViewModel> CreateAsync(HttpContext httpContext, Category category)
    {
      return new CategoryPageViewModel()
      {
        Name = category.Name.GetLocalizationValue(),
        Description = category.Description.GetLocalizationValue(),
        Title = category.Title.GetLocalizationValue(),
        MetaDescription = category.MetaDescription.GetLocalizationValue(),
        MetaKeywords = category.MetaKeywords.GetLocalizationValue(),
        Products = await this.GetProductsAsync(httpContext, category)
      };
    }

    private async Task<IEnumerable<ProductViewModel>> GetProductsAsync(HttpContext httpContext, Category category)
    {
      IProductProvider productProvider = StringActivator.CreateInstance<IProductProvider>(category.ProductProviderCSharpClassName);

      return (await productProvider.GetProductsAsync(httpContext, category)).Select(
        p => new ProductViewModelFactory().Create(p)
      );
    }
  }
}