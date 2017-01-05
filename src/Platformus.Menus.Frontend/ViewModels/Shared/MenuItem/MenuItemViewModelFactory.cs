// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Globalization;
using Platformus.Globalization.Frontend.ViewModels;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Models;

namespace Platformus.Menus.Frontend.ViewModels.Shared
{
  public class MenuItemViewModelFactory : ViewModelFactoryBase
  {
    public MenuItemViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public MenuItemViewModel Create(MenuItem menuItem)
    {
      return new MenuItemViewModel()
      {
        Name = this.GetLocalizationValue(menuItem.NameId),
        Url = GlobalizedUrlFormatter.Format(this.RequestHandler.Storage, menuItem.Url),
        MenuItems = this.RequestHandler.Storage.GetRepository<IMenuItemRepository>().FilteredByMenuItemId(menuItem.Id).Select(
          mi => new MenuItemViewModelFactory(this.RequestHandler).Create(mi)
        )
      };
    }

    public MenuItemViewModel Create(CachedMenuItem cachedMenuItem)
    {
      IEnumerable<CachedMenuItem> cachedMenuItems = new CachedMenuItem[] { };

      if (!string.IsNullOrEmpty(cachedMenuItem.CachedMenuItems))
        cachedMenuItems = JsonConvert.DeserializeObject<IEnumerable<CachedMenuItem>>(cachedMenuItem.CachedMenuItems);

      return new MenuItemViewModel()
      {
        Name = cachedMenuItem.Name,
        Url = GlobalizedUrlFormatter.Format(this.RequestHandler.Storage, cachedMenuItem.Url),
        MenuItems = cachedMenuItems.OrderBy(cmi => cmi.Position).Select(
          cmi => new MenuItemViewModelFactory(this.RequestHandler).Create(cmi)
        )
      };
    }
  }
}