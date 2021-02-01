// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.ECommerce.Filters
{
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
  }
}