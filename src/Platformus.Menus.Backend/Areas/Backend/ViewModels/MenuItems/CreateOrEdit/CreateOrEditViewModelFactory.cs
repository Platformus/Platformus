// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Backend.ViewModels.MenuItems
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = this.GetLocalizations()
        };

      MenuItem menuItem = this.RequestHandler.Storage.GetRepository<IMenuItemRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = menuItem.Id,
        NameLocalizations = this.GetLocalizations(menuItem.NameId),
        Url = menuItem.Url,
        Position = menuItem.Position
      };
    }
  }
}