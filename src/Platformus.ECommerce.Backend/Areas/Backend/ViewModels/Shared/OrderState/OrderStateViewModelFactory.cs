// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class OrderStateViewModelFactory : ViewModelFactoryBase
  {
    public OrderStateViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public OrderStateViewModel Create(OrderState orderState)
    {
      return new OrderStateViewModel()
      {
        Id = orderState.Id,
        Code = orderState.Code,
        Name = this.GetLocalizationValue(orderState.NameId),
        Position = orderState.Position
      };
    }
  }
}