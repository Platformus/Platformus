// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class OrderStateViewModelFactory : ViewModelFactoryBase
  {
    public OrderStateViewModel Create(HttpContext httpContext, OrderState orderState)
    {
      return new OrderStateViewModel()
      {
        Id = orderState.Id,
        Name = orderState.Name.GetLocalizationValue(httpContext),
        Position = orderState.Position
      };
    }
  }
}