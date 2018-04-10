// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce
{
  public static class OrderExtensions
  {
    public static decimal GetTotal(this Order order, IRequestHandler requestHandler)
    {
      decimal total = 0m;

      foreach (Cart cart in requestHandler.Storage.GetRepository<ICartRepository>().FilteredByOrderId(order.Id))
        total += cart.GetTotal(requestHandler);

      return total;
    }
  }
}