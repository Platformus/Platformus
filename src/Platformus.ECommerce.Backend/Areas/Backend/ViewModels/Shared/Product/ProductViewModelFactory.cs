// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Shared;

public static class ProductViewModelFactory
{
  public static ProductViewModel Create(Product product)
  {
    return new ProductViewModel()
    {
      Id = product.Id,
      Category = CategoryViewModelFactory.Create(product.Category),
      Name = product.Name.GetLocalizationValue(),
      Units = product.Units.GetLocalizationValue(),
      Price = product.Price
    };
  }
}