// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Core.Backend;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Primitives;

namespace Platformus.Website.Backend.ViewModels.MenuItems
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }

    [Multilingual]
    [Display(Name = "Name")]
    [Required]
    [StringLength(64)]
    public string Name { get; set; }
    public IEnumerable<Localization> NameLocalizations { get; set; }

    [Display(Name = "URL")]
    [Required]
    [StringLength(128)]
    public string Url { get; set; }

    [Display(Name = "Position")]
    public int? Position { get; set; }
  }
}