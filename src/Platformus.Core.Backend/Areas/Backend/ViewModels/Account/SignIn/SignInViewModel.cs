﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace Platformus.Core.Backend.ViewModels.Account
{
  public class SignInViewModel : ViewModelBase
  {
    public string Next { get; set; }

    [Display(Name = "Email")]
    [Required]
    [StringLength(64)]
    public string Email { get; set; }

    [Display(Name = "Password")]
    [Required]
    [StringLength(64)]
    public string Password { get; set; }

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
  }
}