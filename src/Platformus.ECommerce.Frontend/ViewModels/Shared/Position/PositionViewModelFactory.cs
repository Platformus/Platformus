// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class PositionViewModelFactory : ViewModelFactoryBase
  {
    public PositionViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public PositionViewModel Create(Position position)
    {
      Product product = this.RequestHandler.Storage.GetRepository<IProductRepository>().WithKey(position.ProductId);

      return new PositionViewModel()
      {
        Product = new ProductViewModelFactory(this.RequestHandler).Create(product)
      };
    }
  }
}