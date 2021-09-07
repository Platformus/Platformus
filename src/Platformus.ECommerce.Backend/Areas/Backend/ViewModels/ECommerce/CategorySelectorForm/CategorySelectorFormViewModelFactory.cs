// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.ECommerce
{
  public static class CategorySelectorFormViewModelFactory
  {
    public static CategorySelectorFormViewModel Create(HttpContext httpContext, IEnumerable<Category> categories, int? categoryId)
    {
      IStringLocalizer<CategorySelectorFormViewModel> localizer = httpContext.GetStringLocalizer<CategorySelectorFormViewModel>();

      return new CategorySelectorFormViewModel()
      {
        TableColumns = new[] {
          new TableTagHelper.Column(localizer["Name"])
        },
        Categories = categories.Select(CategoryViewModelFactory.Create),
        CategoryId = categoryId
      };
    }
  }
}