﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Platformus.Core.Backend.ViewModels;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class CategoryViewModelFactory : ViewModelFactoryBase
  {
    public CategoryViewModel Create(Category category)
    {
      return new CategoryViewModel()
      {
        Id = category.Id,
        Name = category.Name.GetLocalizationValue(),
        Categories = category.Categories == null? Array.Empty<CategoryViewModel>() : category.Categories.Select(
          c => new CategoryViewModelFactory().Create(c)
        )
      };
    }
  }
}