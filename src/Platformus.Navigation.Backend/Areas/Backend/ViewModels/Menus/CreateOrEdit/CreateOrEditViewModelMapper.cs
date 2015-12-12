// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Navigation.Data.Abstractions;
using Platformus.Navigation.Data.Models;

namespace Platformus.Navigation.Backend.ViewModels.Menus
{
  public class CreateOrEditViewModelMapper : ViewModelBuilderBase
  {
    public CreateOrEditViewModelMapper(IHandler handler)
      : base(handler)
    {
    }

    public Menu Map(CreateOrEditViewModel createOrEdit)
    {
      Menu menu = new Menu();

      if (createOrEdit.Id != null)
        menu = this.handler.Storage.GetRepository<IMenuRepository>().WithKey((int)createOrEdit.Id);

      menu.Code = createOrEdit.Code;
      return menu;
    }
  }
}