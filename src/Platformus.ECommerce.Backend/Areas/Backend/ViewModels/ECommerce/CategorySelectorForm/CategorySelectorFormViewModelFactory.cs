// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.ECommerce;

public static class CategorySelectorFormViewModelFactory
{
  public static CategorySelectorFormViewModel Create(IEnumerable<Category> categories, int? categoryId)
  {
    return new CategorySelectorFormViewModel()
    {
      Categories = categories.Select(CategoryViewModelFactory.Create).ToList(),
      CategoryId = categoryId
    };
  }
}