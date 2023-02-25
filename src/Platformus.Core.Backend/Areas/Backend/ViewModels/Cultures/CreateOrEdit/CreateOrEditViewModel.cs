﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace Platformus.Core.Backend.ViewModels.Cultures;

public class CreateOrEditViewModel : ViewModelBase
{
  [Display(Name = "Two-letter language code (ISO 639‑1)")]
  [Required]
  [StringLength(2)]
  public string Id { get; set; }

  [Display(Name = "Name")]
  [Required]
  [StringLength(64)]
  public string Name { get; set; }

  [Display(Name = "Is frontend default")]
  public bool IsFrontendDefault { get; set; }

  [Display(Name = "Is backend default")]
  public bool IsBackendDefault { get; set; }
}