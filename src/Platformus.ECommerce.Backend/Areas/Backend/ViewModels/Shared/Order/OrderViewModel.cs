// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class OrderViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerPhone { get; set; }
    public OrderStateViewModel OrderState { get; set; }
    public PaymentMethodViewModel PaymentMethod { get; set; }
    public DeliveryMethodViewModel DeliveryMethod { get; set; }
    public decimal Total { get; set; }
    public DateTime Created { get; set; }
  }
}