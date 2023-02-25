// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.ECommerce.Filters;

public class OrderFilter : IFilter
{
  public int? Id { get; set; }
  public OrderStateFilter OrderState { get; set; }
  public DeliveryMethodFilter DeliveryMethod { get; set; }
  public PaymentMethodFilter PaymentMethod { get; set; }
  public StringFilter CustomerFirstName { get; set; }
  public StringFilter CustomerLastName { get; set; }
  public StringFilter CustomerPhone { get; set; }
  public StringFilter CustomerEmail { get; set; }
  public StringFilter CustomerAddress { get; set; }
  public StringFilter Note { get; set; }
  public DateTimeFilter Created { get; set; }

  public OrderFilter() { }

  public OrderFilter(int? id = null, OrderStateFilter orderState = null, DeliveryMethodFilter deliveryMethod = null, PaymentMethodFilter paymentMethod = null, StringFilter customerFirstName = null, StringFilter customerLastName = null, StringFilter customerPhone = null, StringFilter customerEmail = null, StringFilter customerAddress = null, StringFilter note = null, DateTimeFilter created = null)
  {
    Id = id;
    OrderState = orderState;
    DeliveryMethod = deliveryMethod;
    PaymentMethod = paymentMethod;
    CustomerFirstName = customerFirstName;
    CustomerLastName = customerLastName;
    CustomerPhone = customerPhone;
    CustomerEmail = customerEmail;
    CustomerAddress = customerAddress;
    Note = note;
    Created = created;
  }
}