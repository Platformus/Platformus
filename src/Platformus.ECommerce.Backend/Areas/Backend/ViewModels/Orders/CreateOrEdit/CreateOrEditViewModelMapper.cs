// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Orders;

public static class CreateOrEditViewModelMapper
{
  public static Order Map(Order order, CreateOrEditViewModel createOrEdit)
  {
    if (order.Id == 0)
      order.Created = DateTime.Now.ToUniversalTime();

    order.OrderStateId = createOrEdit.OrderStateId;
    order.DeliveryMethodId = createOrEdit.DeliveryMethodId;
    order.PaymentMethodId = createOrEdit.PaymentMethodId;
    order.CustomerFirstName = createOrEdit.CustomerFirstName;
    order.CustomerLastName = createOrEdit.CustomerLastName;
    order.CustomerPhone = createOrEdit.CustomerPhone;
    order.CustomerEmail = createOrEdit.CustomerEmail;
    order.CustomerAddress = createOrEdit.CustomerAddress;
    order.Note = createOrEdit.Note;
    return order;
  }
}