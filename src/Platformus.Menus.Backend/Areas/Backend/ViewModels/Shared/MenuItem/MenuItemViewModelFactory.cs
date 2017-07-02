// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Backend.ViewModels.Shared
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
        Id = menuItem.Id,
        Name = this.GetLocalizationValue(menuItem.NameId),
        MenuItems = this.RequestHandler.Storage.GetRepository<IMenuItemRepository>().FilteredByMenuItemId(menuItem.Id).ToList().Select(
          mi => new MenuItemViewModelFactory(this.RequestHandler).Create(mi)
        )
      };
    }
  }
}