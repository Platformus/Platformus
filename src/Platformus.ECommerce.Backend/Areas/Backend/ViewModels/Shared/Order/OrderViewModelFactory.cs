// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class OrderViewModelFactory : ViewModelFactoryBase
  {
    public OrderViewModel Create(Order order)
    {
      return new OrderViewModel()
      {
        Id = order.Id,
        OrderState = new OrderStateViewModelFactory().Create(order.OrderState),
        DeliveryMethod = new DeliveryMethodViewModelFactory().Create(order.DeliveryMethod),
        PaymentMethod = new PaymentMethodViewModelFactory().Create(order.PaymentMethod),
        CustomerFirstName = order.CustomerFirstName,
        CustomerLastName = order.CustomerLastName,
        CustomerPhone = order.CustomerPhone,
        Total = order.GetTotal(),
        Created = order.Created
      };
    }
  }
}