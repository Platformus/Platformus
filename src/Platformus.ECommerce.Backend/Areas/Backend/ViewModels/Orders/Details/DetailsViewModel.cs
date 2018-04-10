// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Orders
{
  public class DetailsViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public OrderStateViewModel OrderState { get; set; }
    public PaymentMethodViewModel PaymentMethod { get; set; }
    public DeliveryMethodViewModel DeliveryMethod { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public string Note { get; set; }
    public DateTime Created { get; set; }
    public CartViewModel Cart { get; set; }
  }
}