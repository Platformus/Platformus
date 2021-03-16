// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Frontend.ViewModels;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class CatalogViewModelFactory : ViewModelFactoryBase
  {
    public CatalogViewModel Create(HttpContext httpContext, IEnumerable<Category> categories, string partialViewName, string additionalCssClass)
    {
      return new CatalogViewModel()
      {
        Categories = categories.Select(c => new CategoryViewModelFactory().Create(httpContext, c)),
        PartialViewName = partialViewName ?? "_Catalog",
        AdditionalCssClass = additionalCssClass
      };
    }
  }
}