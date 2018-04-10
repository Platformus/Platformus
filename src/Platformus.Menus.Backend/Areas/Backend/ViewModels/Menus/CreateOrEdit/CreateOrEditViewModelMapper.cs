// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Backend.ViewModels.Menus
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Menu Map(CreateOrEditViewModel createOrEdit)
    {
      Menu menu = new Menu();

      if (createOrEdit.Id != null)
        menu = this.RequestHandler.Storage.GetRepository<IMenuRepository>().WithKey((int)createOrEdit.Id);

      menu.Code = createOrEdit.Code;
      return menu;
    }
  }
}