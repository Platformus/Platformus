// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public class MenuViewModelFactory : ViewModelFactoryBase
  {
    public MenuViewModel Create(HttpContext httpContext, Menu menu)
    {
      return new MenuViewModel()
      {
        Id = menu.Id,
        Name = menu.Name.GetLocalizationValue(httpContext),
        MenuItems = menu.MenuItems.Select(
          mi => new MenuItemViewModelFactory().Create(httpContext, mi)
        )
      };
    }
  }
}