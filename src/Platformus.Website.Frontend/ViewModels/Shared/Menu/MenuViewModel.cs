// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Frontend.ViewModels;

namespace Platformus.Website.Frontend.ViewModels.Shared;

public class MenuViewModel : ViewModelBase
{
  public IEnumerable<MenuItemViewModel> MenuItems { get; set; }
  public string PartialViewName { get; set; }
  public string AdditionalCssClass { get; set; }
}