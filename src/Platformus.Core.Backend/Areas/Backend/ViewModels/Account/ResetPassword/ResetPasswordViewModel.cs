﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace Platformus.Core.Backend.ViewModels.Account;

public class ResetPasswordViewModel : ViewModelBase
{
  [Display(Name = "Email")]
  [Required]
  [StringLength(64)]
  public string Email { get; set; }
}