// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Checkout
{
  public class IndexViewModelMapper : ViewModelMapperBase
  {
    public IndexViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Order Map(IndexViewModel indexViewModel)
    {
      Order order = new Order();

      order.OrderStateId = this.RequestHandler.Storage.GetRepository<IOrderStateRepository>().WithCode("New").Id;
      order.PaymentMethodId = this.RequestHandler.Storage.GetRepository<IPaymentMethodRepository>().WithCode("NotSet").Id;
      order.DeliveryMethodId = this.RequestHandler.Storage.GetRepository<IDeliveryMethodRepository>().WithCode("NotSet").Id;
      order.CustomerFirstName = indexViewModel.CustomerFirstName;
      order.CustomerLastName = indexViewModel.CustomerLastName;
      order.CustomerPhone = indexViewModel.CustomerPhone;
      order.CustomerEmail = indexViewModel.CustomerEmail;
      order.CustomerAddress = indexViewModel.CustomerAddress;
      order.Note = indexViewModel.Note;
      order.Created = DateTime.Now;
      return order;
    }
  }
}