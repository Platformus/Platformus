// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce
{
  /// <summary>
  /// Contains the extension methods of the <see cref="Cart"/>.
  /// </summary>
  public static class CartExtensions
  {
    /// <summary>
    /// Sums up a given cart positions' subtotals.
    /// </summary>
    /// <param name="cart">A cart to sum up positions' subtotals of.</param>
    public static decimal GetTotal(this Cart cart)
    {
      return cart.Positions.Sum(p => p.Price * p.Quantity);
    }
  }
}