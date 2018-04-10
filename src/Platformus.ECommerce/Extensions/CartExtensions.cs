// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce
{
  public static class CartExtensions
  {
    public static decimal GetTotal(this Cart cart, IRequestHandler requestHandler)
    {
      decimal total = 0m;

      foreach (Position position in requestHandler.Storage.GetRepository<IPositionRepository>().FilteredByCartId(cart.Id))
      {
        Product product = requestHandler.Storage.GetRepository<IProductRepository>().WithKey(position.ProductId);

        total += product.Price;
      }

      return total;
    }
  }
}