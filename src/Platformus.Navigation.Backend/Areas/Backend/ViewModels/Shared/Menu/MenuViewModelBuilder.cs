// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Navigation.Data.Abstractions;
using Platformus.Navigation.Data.Models;

namespace Platformus.Navigation.Backend.ViewModels.Shared
{
  public class MenuViewModelBuilder : ViewModelBuilderBase
  {
    public MenuViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public MenuViewModel Build(Menu menu)
    {
      return new MenuViewModel()
      {
        Id = menu.Id,
        Name = this.GetLocalizationValue(menu.NameId),
        MenuItems = this.handler.Storage.GetRepository<IMenuItemRepository>().FilteredByMenuId(menu.Id).Select(
          mi => new MenuItemViewModelBuilder(this.handler).Build(mi)
        )
      };
    }
  }
}