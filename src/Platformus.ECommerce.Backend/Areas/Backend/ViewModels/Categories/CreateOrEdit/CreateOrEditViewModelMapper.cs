// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Categories;

public static class CreateOrEditViewModelMapper
{
  public static Category Map(CategoryFilter filter, Category category, CreateOrEditViewModel createOrEdit)
  {
    if (category.Id == 0)
      category.CategoryId = filter?.Owner?.Id?.Equals;

    category.Url = createOrEdit.Url;
    category.Position = createOrEdit.Position;
    category.ProductProviderCSharpClassName = createOrEdit.ProductProviderCSharpClassName;
    category.ProductProviderParameters = createOrEdit.ProductProviderParameters;
    return category;
  }
}