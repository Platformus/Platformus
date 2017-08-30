// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Globalization.Frontend;
using Platformus.Globalization.Frontend.ViewModels;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Frontend.ViewModels.Shared
{
  public class MenuItemViewModelFactory : ViewModelFactoryBase
  {
    public MenuItemViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public MenuItemViewModel Create(SerializedMenuItem serializedMenuItem)
    {
      IEnumerable<SerializedMenuItem> cachedMenuItems = new SerializedMenuItem[] { };

      if (!string.IsNullOrEmpty(serializedMenuItem.SerializedMenuItems))
        cachedMenuItems = JsonConvert.DeserializeObject<IEnumerable<SerializedMenuItem>>(serializedMenuItem.SerializedMenuItems);

      return new MenuItemViewModel()
      {
        Name = serializedMenuItem.Name,
        Url = GlobalizedUrlFormatter.Format(this.RequestHandler, serializedMenuItem.Url),
        MenuItems = cachedMenuItems.OrderBy(cmi => cmi.Position).Select(
          cmi => new MenuItemViewModelFactory(this.RequestHandler).Create(cmi)
        ).ToList()
      };
    }
  }
}