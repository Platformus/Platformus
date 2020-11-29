// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Frontend;
using Platformus.Core.Frontend.ViewModels;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class CatalogViewModelFactory : ViewModelFactoryBase
  {
    public CatalogViewModel Create(HttpContext httpContext, Catalog catalog)
    {
      return new CatalogViewModel()
      {
        Url = GlobalizedUrlFormatter.Format(httpContext, catalog.Url),
        Name = catalog.Name.GetLocalizationValue(httpContext),
        Catalogs = catalog.Catalogs == null ? Array.Empty<CatalogViewModel>() : catalog.Catalogs.OrderBy(c => c.Position).Select(
          c => new CatalogViewModelFactory().Create(httpContext, c)
        ).ToArray()
      };
    }
  }
}