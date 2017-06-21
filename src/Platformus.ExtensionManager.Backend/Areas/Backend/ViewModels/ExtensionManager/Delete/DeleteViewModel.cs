// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.ExtensionManager.Backend.ViewModels.ExtensionManager
{
  public class DeleteViewModel : ViewModelBase
  {
    [Display(Name = "Id")]
    public string Id { get; set; }

    [Display(Name = "Files")]
    public string Files { get; set; }
  }
}