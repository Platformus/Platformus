// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Categories
{
  public class CreateOrEditViewModelMapper : ViewModelFactoryBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Category Map(CreateOrEditViewModel createOrEdit)
    {
      Category category = new Category();

      if (createOrEdit.Id != null)
        category = this.RequestHandler.Storage.GetRepository<ICategoryRepository>().WithKey((int)createOrEdit.Id);

      else category.CategoryId = createOrEdit.CategoryId;

      category.Position = createOrEdit.Position;
      return category;
    }
  }
}