// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Carts
{
  public static class ViewViewModelFactory
  {
    public static ViewViewModel Create(Cart cart)
    {
      return new ViewViewModel()
      {
        Id = cart.Id,
        Positions = cart.Positions.Select(PositionViewModelFactory.Create),
        Total = cart.GetTotal()
      };
    }
  }
}