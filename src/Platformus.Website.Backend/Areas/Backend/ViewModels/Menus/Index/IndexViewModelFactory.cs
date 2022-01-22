// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Menus
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(IEnumerable<Menu> menus)
    {
      return new IndexViewModel()
      {
        Menus = menus.Select(MenuViewModelFactory.Create).ToList()
      };
    }
  }
}