// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Backend.ViewModels.Shared;

namespace Platformus.Security.Backend.ViewModels.Roles
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }

    [Display(Name = "Code")]
    [Required]
    [StringLength(32)]
    public string Code { get; set; }

    [Display(Name = "Name")]
    [Required]
    [StringLength(64)]
    public string Name { get; set; }

    [Display(Name = "Position")]
    public int? Position { get; set; }
    
    public IEnumerable<RolePermissionViewModel> RolePermissions { get; set; }
  }
}