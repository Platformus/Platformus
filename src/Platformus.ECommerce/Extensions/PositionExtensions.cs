// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce
{
  public static class PositionExtensions
  {
    public static decimal GetSubtotal(this Position position)
    {
      return position.Price * position.Quantity;
    }
  }
}