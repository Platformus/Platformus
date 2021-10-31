// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Platformus.ECommerce.Services.Abstractions
{
  /// <summary>
  /// Describes a cart manager to manipulate the current cart.
  /// </summary>
  public interface ICartManager
  {
    /// <summary>
    /// Returns false if the current cart doesn't contain any positions.
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// Gets the current cart's client-side ID.
    /// </summary>
    /// <param name="clientSideId">An out variable to set the current cart's client-side ID to.</param>
    /// <returns>Returns false if the current cart is empty.</returns>
    bool TryGetClientSideId(out Guid clientSideId);

    /// <summary>
    /// Sums up the current cart total positions' quantity.
    /// </summary>
    Task<int> GetQuantityAsync();

    /// <summary>
    /// Sums up the current cart total positions' subtotals.
    /// </summary>
    Task<decimal> GetTotalAsync();
  }
}