// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Platformus.Core.Backend.ViewModels;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class CatalogViewModelFactory : ViewModelFactoryBase
  {
    public CatalogViewModel Create(Catalog catalog)
    {
      return new CatalogViewModel()
      {
        Id = catalog.Id,
        Name = catalog.Name.GetLocalizationValue(),
        Catalogs = catalog.Catalogs == null ? Array.Empty<CatalogViewModel>() : catalog.Catalogs.Select(
          c => new CatalogViewModelFactory().Create(c)
        )
      };
    }
  }
}