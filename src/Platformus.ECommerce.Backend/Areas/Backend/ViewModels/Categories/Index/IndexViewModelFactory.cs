﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Core.Backend.ViewModels;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Categories
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(IEnumerable<Category> categories)
    {
      return new IndexViewModel()
      {
        Categories = categories.Select(c => new CategoryViewModelFactory().Create(c))
      };
    }
  }
}