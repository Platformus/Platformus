// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Barebone.Primitives;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Orders
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }

    [Display(Name = "Order state")]
    [Required]
    public int OrderStateId { get; set; }
    public IEnumerable<Option> OrderStateOptions { get; set; }

    [Display(Name = "Payment method")]
    [Required]
    public int PaymentMethodId { get; set; }
    public IEnumerable<Option> PaymentMethodOptions { get; set; }

    [Display(Name = "Delivery method")]
    [Required]
    public int DeliveryMethodId { get; set; }
    public IEnumerable<Option> DeliveryMethodOptions { get; set; }

    [Display(Name = "Customer first name")]
    [Required]
    [StringLength(64)]
    public string CustomerFirstName { get; set; }

    [Display(Name = "Customer last name")]
    [Required]
    [StringLength(64)]
    public string CustomerLastName { get; set; }

    [Display(Name = "Customer phone")]
    [Required]
    [StringLength(32)]
    public string CustomerPhone { get; set; }

    [Display(Name = "Customer email")]
    [Required]
    [StringLength(64)]
    public string CustomerEmail { get; set; }

    [Display(Name = "Customer address")]
    [Required]
    [StringLength(128)]
    public string CustomerAddress { get; set; }

    [Display(Name = "Note")]
    [StringLength(1024)]
    public string Note { get; set; }
  }
}