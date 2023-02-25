// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Frontend.ViewModels.Shared;

public static class MenuViewModelFactory
{
  public static MenuViewModel Create(HttpContext httpContext, Menu menu, string partialViewName, string additionalCssClass)
  {
    return new MenuViewModel()
    {
      MenuItems = menu.MenuItems.OrderBy(mi => mi.Position).Select(
        mi => MenuItemViewModelFactory.Create(httpContext, mi)
      ),
      PartialViewName = partialViewName ?? "_Menu",
      AdditionalCssClass = additionalCssClass
    };
  }
}