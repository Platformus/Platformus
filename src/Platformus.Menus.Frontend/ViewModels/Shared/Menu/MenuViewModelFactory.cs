// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.Globalization.Frontend.ViewModels;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Frontend.ViewModels.Shared
{
  public class MenuViewModelFactory : ViewModelFactoryBase
  {
    public MenuViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public MenuViewModel Create(SerializedMenu serializedMenu, string partialViewName, string additionalCssClass)
    {
      IEnumerable<SerializedMenuItem> serializedMenuItems = new SerializedMenuItem[] { };

      if (!string.IsNullOrEmpty(serializedMenu.SerializedMenuItems))
        serializedMenuItems = JsonConvert.DeserializeObject<IEnumerable<SerializedMenuItem>>(serializedMenu.SerializedMenuItems);

      return new MenuViewModel()
      {
        MenuItems = serializedMenuItems.OrderBy(cmi => cmi.Position).Select(
          smi => new MenuItemViewModelFactory(this.RequestHandler).Create(smi)
        ).ToList(),
        PartialViewName = partialViewName ?? "_Menu",
        AdditionalCssClass = additionalCssClass
      };
    }
  }
}