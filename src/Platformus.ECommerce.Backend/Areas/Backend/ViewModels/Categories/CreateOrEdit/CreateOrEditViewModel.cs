// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Core.Primitives;
using Platformus.Core.Backend;
using Platformus.Core.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Categories
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }
    public int? CategoryId { get; set; }

    [Multilingual]
    [Display(Name = "Name")]
    [Required]
    [StringLength(64)]
    public string Name { get; set; }
    public IEnumerable<Localization> NameLocalizations { get; set; }

    [Display(Name = "Position")]
    public int? Position { get; set; }
  }
}