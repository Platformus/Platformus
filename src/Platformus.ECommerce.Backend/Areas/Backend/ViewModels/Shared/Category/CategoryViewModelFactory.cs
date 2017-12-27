// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class CategoryViewModelFactory : ViewModelFactoryBase
  {
    public CategoryViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CategoryViewModel Create(Category category)
    {
      return new CategoryViewModel()
      {
        Id = category.Id,
        Name = this.GetLocalizationValue(category.NameId),
        Categories = this.RequestHandler.Storage.GetRepository<ICategoryRepository>().FilteredByCategoryId(category.Id).ToList().Select(
          c => new CategoryViewModelFactory(this.RequestHandler).Create(c)
        )
      };
    }
  }
}