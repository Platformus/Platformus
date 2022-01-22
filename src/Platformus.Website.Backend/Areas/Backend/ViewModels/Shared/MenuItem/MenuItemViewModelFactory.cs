// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public static class MenuItemViewModelFactory
  {
    public static MenuItemViewModel Create(MenuItem menuItem)
    {
      return new MenuItemViewModel()
      {
        Id = menuItem.Id,
        Name = menuItem.Name.GetLocalizationValue(),
        MenuItems = menuItem.MenuItems == null ?
          Array.Empty<MenuItemViewModel>() :
          menuItem.MenuItems
            .OrderBy(mi => mi.Position)
            .Select(MenuItemViewModelFactory.Create).ToList()
      };
    }
  }
}