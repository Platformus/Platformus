// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Frontend;
using Platformus.Core.Frontend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Frontend.ViewModels.Shared
{
  public class MenuItemViewModelFactory : ViewModelFactoryBase
  {
    public MenuItemViewModel Create(HttpContext httpContext, MenuItem menuItem)
    {
      return new MenuItemViewModel()
      {
        Name = menuItem.Name.GetLocalizationValue(httpContext),
        Url = GlobalizedUrlFormatter.Format(httpContext, menuItem.Url),
        MenuItems = menuItem.MenuItems == null ? Array.Empty<MenuItemViewModel>() : menuItem.MenuItems.OrderBy(cmi => cmi.Position).Select(
          mi => new MenuItemViewModelFactory().Create(httpContext, mi)
        ).ToArray()
      };
    }
  }
}