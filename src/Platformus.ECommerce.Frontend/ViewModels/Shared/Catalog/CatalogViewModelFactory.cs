// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared;

public static class CatalogViewModelFactory
{
  public static CatalogViewModel Create(HttpContext httpContext, IEnumerable<Category> categories, string partialViewName, string additionalCssClass)
  {
    return new CatalogViewModel()
    {
      Categories = categories.Select(c => CategoryViewModelFactory.Create(httpContext, c)),
      PartialViewName = partialViewName ?? "_Catalog",
      AdditionalCssClass = additionalCssClass
    };
  }
}