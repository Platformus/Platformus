// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.Menus.Frontend.ViewModels.Shared
{
  public class MenuItemViewModel : ViewModelBase
  {
    public string Name { get; set; }
    public string Url { get; set; }
    public IEnumerable<MenuItemViewModel> MenuItems { get; set; }
    public int Level { get; set; }
  }
}