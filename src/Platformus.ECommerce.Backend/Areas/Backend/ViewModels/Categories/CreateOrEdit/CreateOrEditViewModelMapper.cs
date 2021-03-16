// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.ViewModels.Categories
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public Category Map(CategoryFilter filter, Category category, CreateOrEditViewModel createOrEdit)
    {
      if (category.Id == 0)
        category.CategoryId = filter?.Owner?.Id?.Equals;

      category.Url = createOrEdit.Url;
      category.CSharpClassName = createOrEdit.CSharpClassName;
      category.Parameters = createOrEdit.Parameters;
      category.Position = createOrEdit.Position;
      return category;
    }
  }
}