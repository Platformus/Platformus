// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Menus.Backend.ViewModels.Shared;

namespace Platformus.Menus.Backend.ViewModels.Menus
{
  public class IndexViewModel : ViewModelBase
  {
    public IEnumerable<MenuViewModel> Menus { get; set; }
  }
}