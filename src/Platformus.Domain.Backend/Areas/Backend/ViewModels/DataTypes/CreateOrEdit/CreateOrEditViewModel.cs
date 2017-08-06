// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Barebone.Primitives;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.DataTypes
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }

    [Display(Name = "Storage data type")]
    [Required]
    [StringLength(32)]
    public string StorageDataType { get; set; }
    public IEnumerable<Option> StorageDataTypeOptions { get; set; }

    [Display(Name = "JavaScript editor class name")]
    [Required]
    [StringLength(128)]
    public string JavaScriptEditorClassName { get; set; }

    [Display(Name = "Name")]
    [Required]
    [StringLength(64)]
    public string Name { get; set; }

    [Display(Name = "Position")]
    public int? Position { get; set; }
  }
}