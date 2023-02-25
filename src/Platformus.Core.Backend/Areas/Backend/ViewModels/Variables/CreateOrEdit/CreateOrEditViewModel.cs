// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace Platformus.Core.Backend.ViewModels.Variables;

public class CreateOrEditViewModel : ViewModelBase
{
  public int? Id { get; set; }

  [Display(Name = "Code")]
  [Required]
  [StringLength(32)]
  [RegularExpression(@"^[a-zA-Z_][a-zA-Z0-9_]*$")]
  public string Code { get; set; }

  [Display(Name = "Name")]
  [Required]
  [StringLength(64)]
  public string Name { get; set; }

  [Display(Name = "Value")]
  [Required]
  [StringLength(1024)]
  public string Value { get; set; }

  [Display(Name = "Position")]
  public int? Position { get; set; }
}