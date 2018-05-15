// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Barebone.Primitives;
using Platformus.Globalization.Backend;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Attributes
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }
    public int FeatureId { get; set; }

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