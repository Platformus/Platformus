// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Frontend;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class CatalogViewModelFactory : ViewModelFactoryBase
  {
    public CatalogViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CatalogViewModel Create(Catalog catalog)
    {
      IEnumerable<Catalog> catalogs = this.RequestHandler.Storage.GetRepository<ICatalogRepository>().FilteredByCatalogId(catalog.Id).ToList();

      return new CatalogViewModel()
      {
        Url = GlobalizedUrlFormatter.Format(this.RequestHandler, catalog.Url),
        Name = this.RequestHandler.GetLocalizationValue(catalog.NameId),
        Catalogs = catalogs.Select(
          c => new CatalogViewModelFactory(this.RequestHandler).Create(c)
        ).ToList()
      };
    }
  }
}