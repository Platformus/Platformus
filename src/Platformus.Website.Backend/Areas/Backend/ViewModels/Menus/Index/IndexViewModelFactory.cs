// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Menus
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(IEnumerable<Menu> menus)
    {
      return new IndexViewModel()
      {
        Menus = menus.Select(m => new MenuViewModelFactory().Create(m))
      };
    }
  }
}