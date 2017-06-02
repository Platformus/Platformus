// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.DataTypeParameters
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }
    public int DataTypeId { get; set; }

    [Display(Name = "JavaScript editor class name")]
    [Required]
    [StringLength(128)]
    public string JavaScriptEditorClassName { get; set; }

    [Display(Name = "Code")]
    [Required]
    [StringLength(32)]
    public string Code { get; set; }

    [Display(Name = "Name")]
    [Required]
    [StringLength(64)]
    public string Name { get; set; }
  }
}