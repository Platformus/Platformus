// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Frontend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Frontend.ViewModels.Shared
{
  public class MenuViewModelFactory : ViewModelFactoryBase
  {
    public MenuViewModel Create(HttpContext httpContext, Menu menu, string partialViewName, string additionalCssClass)
    {
      return new MenuViewModel()
      {
        MenuItems = menu.MenuItems.OrderBy(mi => mi.Position).Select(
          mi => new MenuItemViewModelFactory().Create(httpContext, mi)
        ),
        PartialViewName = partialViewName ?? "_Menu",
        AdditionalCssClass = additionalCssClass
      };
    }
  }
}