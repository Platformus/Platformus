﻿// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Core.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class OrderViewModelFactory : ViewModelFactoryBase
  {
    public OrderViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public OrderViewModel Create(Order order)
    {
      OrderState orderState = this.RequestHandler.Storage.GetRepository<IOrderStateRepository>().WithKey(order.OrderStateId);
      PaymentMethod paymentMethod = this.RequestHandler.Storage.GetRepository<IPaymentMethodRepository>().WithKey(order.PaymentMethodId);
      DeliveryMethod deliveryMethod = this.RequestHandler.Storage.GetRepository<IDeliveryMethodRepository>().WithKey(order.DeliveryMethodId);

      return new OrderViewModel()
      {
        Id = order.Id,
        CustomerFirstName = order.CustomerFirstName,
        CustomerLastName = order.CustomerLastName,
        CustomerPhone = order.CustomerPhone,
        OrderState = new OrderStateViewModelFactory(this.RequestHandler).Create(orderState),
        PaymentMethod = new PaymentMethodViewModelFactory(this.RequestHandler).Create(paymentMethod),
        DeliveryMethod = new DeliveryMethodViewModelFactory(this.RequestHandler).Create(deliveryMethod),
        Total = order.GetTotal(this.RequestHandler),
        Created = order.Created
      };
    }
  }
}