// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Checkout
{
  public class IndexViewModel : ViewModelBase
  {
    [Display(Name = "Your first name")]
    [Required]
    [StringLength(64)]
    public string CustomerFirstName { get; set; }

    [Display(Name = "Your last name")]
    [Required]
    [StringLength(64)]
    public string CustomerLastName { get; set; }

    [Display(Name = "Your phone")]
    [Required]
    [StringLength(32)]
    public string CustomerPhone { get; set; }

    [Display(Name = "Your email")]
    [Required]
    [StringLength(64)]
    public string CustomerEmail { get; set; }

    [Display(Name = "Your address")]
    [Required]
    [StringLength(128)]
    public string CustomerAddress { get; set; }

    [Display(Name = "Note")]
    [StringLength(1024)]
    public string Note { get; set; }
  }
}