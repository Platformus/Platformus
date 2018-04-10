// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class ProductViewModelFactory : ViewModelFactoryBase
  {
    public ProductViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ProductViewModel Create(Product product)
    {
      return new ProductViewModel()
      {
        Id = product.Id,
        Name = this.RequestHandler.GetLocalizationValue(product.NameId)
      };
    }
  }
}