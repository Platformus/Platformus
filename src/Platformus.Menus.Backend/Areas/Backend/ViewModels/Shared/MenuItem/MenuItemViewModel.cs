// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Menus.Backend.ViewModels.Shared
{
  public class MenuItemViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<MenuItemViewModel> MenuItems { get; set; }
  }
}