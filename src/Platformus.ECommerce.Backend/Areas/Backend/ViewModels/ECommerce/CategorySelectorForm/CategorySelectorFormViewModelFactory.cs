// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.ECommerce
{
  public class CategorySelectorFormViewModelFactory : ViewModelFactoryBase
  {
    public CategorySelectorFormViewModel Create(HttpContext httpContext, IEnumerable<Category> categories, int? categoryId)
    {
      IStringLocalizer<CategorySelectorFormViewModelFactory> localizer = httpContext.GetStringLocalizer<CategorySelectorFormViewModelFactory>();

      return new CategorySelectorFormViewModel()
      {
        GridColumns = new[] {
          new GridColumnViewModelFactory().Create(localizer["Name"])
        },
        Categories = categories.Select(c => new CategoryViewModelFactory().Create(c)),
        CategoryId = categoryId
      };
    }
  }
}