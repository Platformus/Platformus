// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Frontend;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Frontend.ViewModels.Shared
{
  public static class MenuItemViewModelFactory
  {
    public static MenuItemViewModel Create(HttpContext httpContext, MenuItem menuItem)
    {
      return new MenuItemViewModel()
      {
        Name = menuItem.Name.GetLocalizationValue(),
        Url = GlobalizedUrlFormatter.Format(httpContext, menuItem.Url),
        MenuItems = menuItem.MenuItems == null ?
          Array.Empty<MenuItemViewModel>() :
          menuItem.MenuItems.OrderBy(mi => mi.Position)
            .Select(mi => MenuItemViewModelFactory.Create(httpContext, mi)).ToList()
      };
    }
  }
}