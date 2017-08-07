// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Primitives;
using Platformus.Routing.Backend.ViewModels.Shared;

namespace Platformus.Routing.Backend.ViewModels.Endpoints
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }

    [Display(Name = "Name")]
    [Required]
    [StringLength(64)]
    public string Name { get; set; }

    [Display(Name = "URL template")]
    [StringLength(128)]
    public string UrlTemplate { get; set; }

    [Display(Name = "Position")]
    public int? Position { get; set; }

    [Display(Name = "Disallow anonymous")]
    [Required]
    public bool DisallowAnonymous { get; set; }

    [Display(Name = "Sign in URL")]
    [StringLength(128)]
    public string SignInUrl { get; set; }

    [Display(Name = "C# class name")]
    [Required]
    [StringLength(128)]
    public string CSharpClassName { get; set; }
    public IEnumerable<Option> CSharpClassNameOptions { get; set; }

    public string Parameters { get; set; }
    public IEnumerable<dynamic> Endpoints { get; set; }

    public IEnumerable<EndpointPermissionViewModel> EndpointPermissions { get; set; }
  }
}