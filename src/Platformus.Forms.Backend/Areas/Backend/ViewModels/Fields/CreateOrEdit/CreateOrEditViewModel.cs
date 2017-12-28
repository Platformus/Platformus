// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Barebone.Primitives;
using Platformus.Globalization.Backend;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Forms.Backend.ViewModels.Fields
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }
    public int FormId { get; set; }

    [Display(Name = "Type")]
    [Required]
    public int FieldTypeId { get; set; }
    public IEnumerable<Option> FieldTypeOptions { get; set; }

    [Display(Name = "Code")]
    [Required]
    [StringLength(32)]
    public string Code { get; set; }

    [Multilingual]
    [Display(Name = "Name")]
    [Required]
    [StringLength(64)]
    public string Name { get; set; }
    public IEnumerable<Localization> NameLocalizations { get; set; }

    [Display(Name = "Is required")]
    [Required]
    public bool IsRequired { get; set; }

    [Display(Name = "Max length")]
    public int? MaxLength { get; set; }

    [Display(Name = "Position")]
    public int? Position { get; set; }
  }
}