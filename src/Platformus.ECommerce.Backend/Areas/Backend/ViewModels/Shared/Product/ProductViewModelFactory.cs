// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class ProductViewModelFactory : ViewModelFactoryBase
  {
    public ProductViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ProductViewModel Create(Product product)
    {
      Category category = this.RequestHandler.Storage.GetRepository<ICategoryRepository>().WithKey(product.CategoryId);

      return new ProductViewModel()
      {
        Id = product.Id,
        Category = new CategoryViewModelFactory(this.RequestHandler).Create(category),
        Code = product.Code,
        Name = this.GetLocalizationValue(product.NameId),
        Price = product.Price
      };
    }
  }
}