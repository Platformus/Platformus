// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.Security.Backend.ViewModels.Account
{
  public class ResetPasswordViewModel : ViewModelBase
  {
    [Display(Name = "Email")]
    [Required]
    [StringLength(64)]
    public string Email { get; set; }
  }
}