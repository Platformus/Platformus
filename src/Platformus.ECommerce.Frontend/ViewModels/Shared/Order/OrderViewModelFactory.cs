// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public static class OrderViewModelFactory
  {
    public static OrderViewModel Create(Order order)
    {
      return new OrderViewModel()
      {
        Id = order.Id,
        PaymentMethod = PaymentMethodViewModelFactory.Create(order.PaymentMethod),
        DeliveryMethod = DeliveryMethodViewModelFactory.Create(order.DeliveryMethod),
        CustomerFirstName = order.CustomerFirstName,
        CustomerLastName = order.CustomerLastName,
        CustomerPhone = order.CustomerPhone,
        CustomerEmail = order.CustomerEmail,
        CustomerAddress = order.CustomerAddress,
        Note = order.Note,
        Positions = order.Positions.Select(PositionViewModelFactory.Create)
      };
    }
  }
}