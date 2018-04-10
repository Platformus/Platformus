// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.ECommerce
{
  public class CategorySelectorFormViewModelFactory : ViewModelFactoryBase
  {
    public CategorySelectorFormViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CategorySelectorFormViewModel Create(int? categoryId)
    {
      IStringLocalizer<CategorySelectorFormViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<CategorySelectorFormViewModelFactory>>();

      return new CategorySelectorFormViewModel()
      {
        GridColumns = new[] {
          new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Name"])
        },
        Categories = this.RequestHandler.Storage.GetRepository<ICategoryRepository>().FilteredByCategoryId(null).Select(
          c => new CategoryViewModelFactory(this.RequestHandler).Create(c)
        ),
        CategoryId = categoryId
      };
    }
  }
}