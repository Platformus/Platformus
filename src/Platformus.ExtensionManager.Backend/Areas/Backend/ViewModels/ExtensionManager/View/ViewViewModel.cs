// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.ExtensionManager.Backend.ViewModels.ExtensionManager
{
  public class ViewViewModel : ViewModelBase
  {
    [Display(Name = "ID")]
    public string Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "Description")]
    public string Description { get; set; }

    [Display(Name = "URL")]
    public string Url { get; set; }

    [Display(Name = "Authors")]
    public string Authors { get; set; }

    [Display(Name = "Version")]
    public string Version { get; set; }
  }
}