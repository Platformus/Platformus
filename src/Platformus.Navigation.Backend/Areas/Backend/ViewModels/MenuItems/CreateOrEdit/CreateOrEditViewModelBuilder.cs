// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Navigation.Data.Abstractions;
using Platformus.Navigation.Data.Models;

namespace Platformus.Navigation.Backend.ViewModels.MenuItems
{
  public class CreateOrEditViewModelBuilder : ViewModelBuilderBase
  {
    public CreateOrEditViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public CreateOrEditViewModel Build(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = this.GetLocalizations()
        };

      MenuItem menuItem = this.handler.Storage.GetRepository<IMenuItemRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = menuItem.Id,
        NameLocalizations = this.GetLocalizations(this.handler.Storage.GetRepository<IDictionaryRepository>().WithKey(menuItem.NameId)),
        Url = menuItem.Url,
        Position = menuItem.Position
      };
    }
  }
}