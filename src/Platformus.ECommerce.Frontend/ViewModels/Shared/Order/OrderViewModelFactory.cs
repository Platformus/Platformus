// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Core.Frontend.ViewModels;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class OrderViewModelFactory : ViewModelFactoryBase
  {
    public OrderViewModel Create(Order order)
    {
      return new OrderViewModel()
      {
        Id = order.Id,
        PaymentMethod = new PaymentMethodViewModelFactory().Create(order.PaymentMethod),
        DeliveryMethod = new DeliveryMethodViewModelFactory().Create(order.DeliveryMethod),
        CustomerFirstName = order.CustomerFirstName,
        CustomerLastName = order.CustomerLastName,
        CustomerPhone = order.CustomerPhone,
        CustomerEmail = order.CustomerEmail,
        CustomerAddress = order.CustomerAddress,
        Note = order.Note,
        Positions = order.Positions.Select(p => new PositionViewModelFactory().Create(p))
      };
    }
  }
}