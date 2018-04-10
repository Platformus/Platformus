// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Orders
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Order Map(CreateOrEditViewModel createOrEdit)
    {
      Order order = new Order();

      if (createOrEdit.Id != null)
        order = this.RequestHandler.Storage.GetRepository<IOrderRepository>().WithKey((int)createOrEdit.Id);

      else order.Created = DateTime.Now;

      order.OrderStateId = createOrEdit.OrderStateId;
      order.PaymentMethodId = createOrEdit.PaymentMethodId;
      order.DeliveryMethodId = createOrEdit.DeliveryMethodId;
      order.CustomerFirstName = createOrEdit.CustomerFirstName;
      order.CustomerLastName = createOrEdit.CustomerLastName;
      order.CustomerPhone = createOrEdit.CustomerPhone;
      order.CustomerEmail = createOrEdit.CustomerEmail;
      order.CustomerAddress = createOrEdit.CustomerAddress;
      order.Note = createOrEdit.Note;
      return order;
    }
  }
}