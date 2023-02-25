// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Platformus.Core.Backend.ViewModels;

namespace Platformus.Website.Backend.ViewModels.Tabs;

public class CreateOrEditViewModel : ViewModelBase
{
  public int? Id { get; set; }

  [Display(Name = "Name")]
  [Required]
  [StringLength(64)]
  public string Name { get; set; }

  [Display(Name = "Position")]
  public int? Position { get; set; }
}