// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class CatalogsViewModelFactory : ViewModelFactoryBase
  {
    public CatalogsViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CatalogsViewModel Create(string additionalCssClass)
    {
      return new CatalogsViewModel()
      {
        Catalogs = this.RequestHandler.Storage.GetRepository<ICatalogRepository>().FilteredByCatalogId(null).ToList().Select(
          c => new CatalogViewModelFactory(this.RequestHandler).Create(c)
        ).ToList(),
        AdditionalCssClass = additionalCssClass
      };
    }
  }
}