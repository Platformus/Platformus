// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public static class CartViewModelFactory
  {
    public static CartViewModel Create(Cart cart)
    {
      return new CartViewModel()
      {
        Id = cart.Id,
        Total = cart.GetTotal(),
        Created = cart.Created
      };
    }
  }
}