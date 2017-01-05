// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Globalization.Frontend.ViewModels;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Models;

namespace Platformus.Menus.Frontend.ViewModels.Shared
{
  public class MenuViewModelFactory : ViewModelFactoryBase
  {
    public MenuViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public MenuViewModel Create(Menu menu)
    {
      return new MenuViewModel()
      {
        MenuItems = this.RequestHandler.Storage.GetRepository<IMenuItemRepository>().FilteredByMenuId(menu.Id).Select(
          mi => new MenuItemViewModelFactory(this.RequestHandler).Create(mi)
        )
      };
    }

    public MenuViewModel Create(CachedMenu cachedMenu)
    {
      IEnumerable<CachedMenuItem> cachedMenuItems = new CachedMenuItem[] { };

      if (!string.IsNullOrEmpty(cachedMenu.CachedMenuItems))
        cachedMenuItems = JsonConvert.DeserializeObject<IEnumerable<CachedMenuItem>>(cachedMenu.CachedMenuItems);

      return new MenuViewModel()
      {
        MenuItems = cachedMenuItems.OrderBy(cmi => cmi.Position).Select(
          cmi => new MenuItemViewModelFactory(this.RequestHandler).Create(cmi)
        )
      };
    }
  }
}