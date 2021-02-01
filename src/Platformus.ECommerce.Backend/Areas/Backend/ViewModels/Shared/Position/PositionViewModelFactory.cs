// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class PositionViewModelFactory : ViewModelFactoryBase
  {
    public PositionViewModel Create(HttpContext httpContext, Position position)
    {
      return new PositionViewModel()
      {
        Id = position.Id,
        Product = new ProductViewModelFactory().Create(httpContext, position.Product),
        Price = position.Price,
        Quantity = position.Quantity,
        Subtotal = position.Price * position.Quantity
      };
    }
  }
}