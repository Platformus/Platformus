// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce
{
  /// <summary>
  /// Contains the extension methods of the <see cref="Order"/>.
  /// </summary>
  public static class OrderExtensions
  {
    /// <summary>
    /// Sums up a given order positions' subtotals.
    /// </summary>
    /// <param name="cart">An order to sum up positions' subtotals of.</param>
    public static decimal GetTotal(this Order order)
    {
      return order.Positions.Sum(p => p.Price * p.Quantity);
    }
  }
}