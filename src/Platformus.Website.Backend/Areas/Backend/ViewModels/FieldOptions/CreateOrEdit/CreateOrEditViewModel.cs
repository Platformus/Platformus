﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Core.Backend;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Primitives;

namespace Platformus.Website.Backend.ViewModels.FieldOptions
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }

    [Multilingual]
    [Display(Name = "Value")]
    [Required]
    [StringLength(64)]
    public string Value { get; set; }
    public IEnumerable<Localization> ValueLocalizations { get; set; }

    [Display(Name = "Position")]
    public int? Position { get; set; }
  }
}