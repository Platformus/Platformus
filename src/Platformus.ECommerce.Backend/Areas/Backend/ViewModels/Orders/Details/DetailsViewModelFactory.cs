// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Orders
{
  public class DetailsViewModelFactory : ViewModelFactoryBase
  {
    public DetailsViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public DetailsViewModel Create(int id)
    {
      Order order = this.RequestHandler.Storage.GetRepository<IOrderRepository>().WithKey((int)id);
      OrderState orderState = this.RequestHandler.Storage.GetRepository<IOrderStateRepository>().WithKey(order.OrderStateId);
      PaymentMethod paymentMethod = this.RequestHandler.Storage.GetRepository<IPaymentMethodRepository>().WithKey(order.PaymentMethodId);
      DeliveryMethod deliveryMethod = this.RequestHandler.Storage.GetRepository<IDeliveryMethodRepository>().WithKey(order.DeliveryMethodId);
      Cart cart = this.RequestHandler.Storage.GetRepository<ICartRepository>().FilteredByOrderId(order.Id).FirstOrDefault();

      return new DetailsViewModel()
      {
        Id = order.Id,
        OrderState = new OrderStateViewModelFactory(this.RequestHandler).Create(orderState),
        PaymentMethod = new PaymentMethodViewModelFactory(this.RequestHandler).Create(paymentMethod),
        DeliveryMethod = new DeliveryMethodViewModelFactory(this.RequestHandler).Create(deliveryMethod),
        CustomerFirstName = order.CustomerFirstName,
        CustomerLastName = order.CustomerLastName,
        CustomerPhone = order.CustomerPhone,
        CustomerEmail = order.CustomerEmail,
        CustomerAddress = order.CustomerAddress,
        Note = order.Note,
        Cart = new CartViewModelFactory(this.RequestHandler).Create(cart, true)
      };
    }
  }
}