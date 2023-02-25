// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.MenuItems;

public static class CreateOrEditViewModelMapper
{
  public static MenuItem Map(MenuItemFilter filter, MenuItem menuItem, CreateOrEditViewModel createOrEdit)
  {
    if (menuItem.Id == 0)
    {
      menuItem.MenuId = filter.Menu?.Id;
      menuItem.MenuItemId = filter.MenuItem?.Id;
    }

    menuItem.Url = createOrEdit.Url;
    menuItem.Position = createOrEdit.Position;
    return menuItem;
  }
}