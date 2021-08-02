// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public static class ProductViewModelFactory
  {
    public static ProductViewModel Create(Product product)
    {
      Photo cover = product.Photos.FirstOrDefault(p => p.IsCover);

      return new ProductViewModel()
      {
        Id = product.Id,
        Url = product.Url,
        Name = product.Name.GetLocalizationValue(),
        Units = product.Units.GetLocalizationValue(),
        Price = product.Price,
        Cover = cover == null ? null : PhotoViewModelFactory.Create(cover)
      };
    }
  }
}