// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Frontend.ViewModels;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Frontend.ViewModels.Shared;

namespace Platformus.ECommerce.Frontend.ViewModels.ECommerce
{
  public class ProductPageViewModelFactory : ViewModelFactoryBase
  {
    public ProductPageViewModel Create(HttpContext httpContext, Product product)
    {
      return new ProductPageViewModel()
      {
        Id = product.Id,
        Category = product.Category == null ? null : new CategoryViewModelFactory().Create(httpContext, product.Category),
        Url = product.Url,
        Name = product.Name.GetLocalizationValue(httpContext),
        Description = product.Description.GetLocalizationValue(httpContext),
        Price = product.Price,
        Title = product.Title.GetLocalizationValue(httpContext),
        MetaDescription = product.MetaDescription.GetLocalizationValue(httpContext),
        MetaKeywords = product.MetaKeywords.GetLocalizationValue(httpContext),
        Photos = product.Photos.OrderBy(p => p.Position).Select(
          p => new PhotoViewModelFactory().Create(p)
        )
      };
    }
  }
}