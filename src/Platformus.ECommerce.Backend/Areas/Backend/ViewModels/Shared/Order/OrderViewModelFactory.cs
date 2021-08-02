// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public static class OrderViewModelFactory
  {
    public static OrderViewModel Create(Order order)
    {
      return new OrderViewModel()
      {
        Id = order.Id,
        OrderState = OrderStateViewModelFactory.Create(order.OrderState),
        DeliveryMethod = DeliveryMethodViewModelFactory.Create(order.DeliveryMethod),
        PaymentMethod = PaymentMethodViewModelFactory.Create(order.PaymentMethod),
        CustomerFirstName = order.CustomerFirstName,
        CustomerLastName = order.CustomerLastName,
        CustomerPhone = order.CustomerPhone,
        Total = order.GetTotal(),
        Created = order.Created
      };
    }
  }
}