// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Platformus.Core.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared;

public class OrderViewModel : ViewModelBase
{
  public int Id { get; set; }
  public PaymentMethodViewModel PaymentMethod { get; set; }
  public DeliveryMethodViewModel DeliveryMethod { get; set; }
  public string CustomerFirstName { get; set; }
  public string CustomerLastName { get; set; }
  public string CustomerPhone { get; set; }
  public string CustomerEmail { get; set; }
  public string CustomerAddress { get; set; }
  public string Note { get; set; }
  public DateTime Created { get; set; }
  public IEnumerable<PositionViewModel> Positions { get; set; }
}