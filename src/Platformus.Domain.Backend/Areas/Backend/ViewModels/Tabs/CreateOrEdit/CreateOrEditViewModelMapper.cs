// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Tabs
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Tab Map(CreateOrEditViewModel createOrEdit)
    {
      Tab tab = new Tab();

      if (createOrEdit.Id != null)
        tab = this.RequestHandler.Storage.GetRepository<ITabRepository>().WithKey((int)createOrEdit.Id);

      else tab.ClassId = createOrEdit.ClassId;

      tab.Name = createOrEdit.Name;
      tab.Position = createOrEdit.Position;
      return tab;
    }
  }
}