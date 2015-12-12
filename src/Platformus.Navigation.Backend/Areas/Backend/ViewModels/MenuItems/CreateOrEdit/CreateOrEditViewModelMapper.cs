// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Navigation.Data.Abstractions;
using Platformus.Navigation.Data.Models;

namespace Platformus.Navigation.Backend.ViewModels.MenuItems
{
  public class CreateOrEditViewModelMapper : ViewModelBuilderBase
  {
    public CreateOrEditViewModelMapper(IHandler handler)
      : base(handler)
    {
    }

    public MenuItem Map(CreateOrEditViewModel createOrEdit)
    {
      MenuItem menuItem = new MenuItem();

      if (createOrEdit.Id != null)
        menuItem = this.handler.Storage.GetRepository<IMenuItemRepository>().WithKey((int)createOrEdit.Id);

      else
      {
        menuItem.MenuId = createOrEdit.MenuId;
        menuItem.MenuItemId = createOrEdit.MenuItemId;
      }

      menuItem.Url = createOrEdit.Url;
      menuItem.Position = createOrEdit.Position;
      return menuItem;
    }
  }
}