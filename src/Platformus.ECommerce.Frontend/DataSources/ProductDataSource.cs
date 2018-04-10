// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Routing.DataSources;

namespace Platformus.ECommerce.Frontend.DataSources
{
  public class ProductDataSource : DataSourceBase
  {
    public override string Description => "Loads current product by URL.";

    protected override dynamic GetData()
    {
      string url = string.Format("/{0}", this.requestHandler.HttpContext.GetRouteValue("url"));
      Product product = this.requestHandler.Storage.GetRepository<IProductRepository>().WithUrl(url);

      if (product == null)
        throw new HttpException(404, "Not found.");

      IEnumerable<Photo> photos = this.requestHandler.Storage.GetRepository<IPhotoRepository>().FilteredByProductId(product.Id).ToList();

      return new ExpandoObjectBuilder()
        .AddProperty("Id", product.Id)
        .AddProperty("Name", this.requestHandler.GetLocalizationValue(product.NameId))
        .AddProperty("Price", product.Price)
        .AddProperty("Photos", photos.Select(ph => this.CreatePhotoViewModel(ph)))
        .Build();
    }

    private dynamic CreatePhotoViewModel(Photo photo)
    {
      return new ExpandoObjectBuilder()
        .AddProperty("Filename", photo.Filename)
        .Build();
    }
  }
}