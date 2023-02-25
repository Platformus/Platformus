// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Core.Frontend.ViewModels;
using Platformus.ECommerce.Frontend.ViewModels.Shared;

namespace Platformus.ECommerce.Frontend.ViewModels.ECommerce;

public class CheckoutPageViewModel : ViewModelBase
{
  [Display(Name = "Payment method")]
  public int PaymentMethodId { get; set; }
  public IEnumerable<PaymentMethodViewModel> PaymentMethods { get; set; }

  [Display(Name = "Delivery method")]
  public int DeliveryMethodId { get; set; }
  public IEnumerable<DeliveryMethodViewModel> DeliveryMethods { get; set; }

  [Display(Name = "First name")]
  [Required]
  [StringLength(64)]
  public string FirstName { get; set; }

  [Display(Name = "Last name")]
  [StringLength(64)]
  public string LastName { get; set; }

  [Display(Name = "Phone")]
  [Required]
  [StringLength(32)]
  public string Phone { get; set; }

  [Display(Name = "Email")]
  [StringLength(64)]
  public string Email { get; set; }

  [Display(Name = "Address")]
  [StringLength(128)]
  public string Address { get; set; }

  [Display(Name = "Note")]
  [StringLength(1024)]
  public string Note { get; set; }
}