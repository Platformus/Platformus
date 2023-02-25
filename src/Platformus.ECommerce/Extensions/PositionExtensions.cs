// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce;

/// <summary>
/// Contains the extension methods of the <see cref="Position"/>.
/// </summary>
public static class PositionExtensions
{
  /// <summary>
  /// Calculates a given position's subtotal (multiplies price by quantity).
  /// </summary>
  /// <param name="position">A position to calculate subtotal of.</param>
  public static decimal GetSubtotal(this Position position)
  {
    return position.Price * position.Quantity;
  }
}