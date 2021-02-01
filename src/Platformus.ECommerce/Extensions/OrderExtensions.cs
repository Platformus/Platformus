// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce
{
  public static class OrderExtensions
  {
    public static decimal GetTotal(this Order order)
    {
      return order.Positions.Sum(p => p.Subtotal);
    }
  }
}