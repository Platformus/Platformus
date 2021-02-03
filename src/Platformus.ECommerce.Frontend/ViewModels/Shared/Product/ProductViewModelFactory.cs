// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Core.Frontend.ViewModels;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class ProductViewModelFactory : ViewModelFactoryBase
  {
    public ProductViewModel Create(Product product)
    {
      Photo cover = product.Photos.FirstOrDefault(p => p.IsCover);

      return new ProductViewModel()
      {
        Id = product.Id,
        Url = product.Url,
        Name = product.Name.GetLocalizationValue(),
        Price = product.Price,
        Cover = cover == null ? null : new PhotoViewModelFactory().Create(cover)
      };
    }
  }
}