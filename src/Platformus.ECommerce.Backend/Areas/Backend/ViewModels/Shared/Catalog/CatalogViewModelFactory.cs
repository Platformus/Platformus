// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class CatalogViewModelFactory : ViewModelFactoryBase
  {
    public CatalogViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CatalogViewModel Create(Catalog catalog)
    {
      return new CatalogViewModel()
      {
        Id = catalog.Id,
        Name = this.GetLocalizationValue(catalog.NameId),
        Catalogs = this.RequestHandler.Storage.GetRepository<ICatalogRepository>().FilteredByCatalogId(catalog.Id).ToList().Select(
          c => new CatalogViewModelFactory(this.RequestHandler).Create(c)
        )
      };
    }
  }
}