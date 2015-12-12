// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Globalization.Frontend.ViewModels;
using Platformus.Navigation.Data.Abstractions;
using Platformus.Navigation.Data.Models;

namespace Platformus.Navigation.Frontend.ViewModels.Shared
{
  public class MenuViewModelBuilder : ViewModelBuilderBase
  {
    public MenuViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public MenuViewModel Build(Menu menu)
    {
      return new MenuViewModel()
      {
        MenuItems = this.handler.Storage.GetRepository<IMenuItemRepository>().FilteredByMenuId(menu.Id).Select(
          mi => new MenuItemViewModelBuilder(this.handler).Build(mi)
        )
      };
    }

    public MenuViewModel Build(CachedMenu cachedMenu)
    {
      IEnumerable<CachedMenuItem> cachedMenuItems = new CachedMenuItem[] { };

      if (!string.IsNullOrEmpty(cachedMenu.CachedMenuItems))
        cachedMenuItems = JsonConvert.DeserializeObject<IEnumerable<CachedMenuItem>>(cachedMenu.CachedMenuItems);

      return new MenuViewModel()
      {
        MenuItems = cachedMenuItems.OrderBy(cmi => cmi.Position).Select(
          cmi => new MenuItemViewModelBuilder(this.handler).Build(cmi)
        )
      };
    }
  }
}