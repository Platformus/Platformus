// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Routing;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.ProductProviders;
using Platformus.Routing.DataSources;

namespace Platformus.ECommerce.Frontend.DataSources
{
  public class CatalogDataSource : DataSourceBase
  {
    public override string Description => "Loads current catalog by URL.";

    protected override dynamic GetData()
    {
      string url = string.Format("/{0}", this.requestHandler.HttpContext.GetRouteValue("url"));
      Catalog catalog = this.requestHandler.Storage.GetRepository<ICatalogRepository>().WithUrl(url);

      if (catalog == null)
        throw new HttpException(404, "Not found.");

      ExpandoObjectBuilder expandoObjectBuilder = new ExpandoObjectBuilder();

      expandoObjectBuilder.AddProperty("Name", this.requestHandler.GetLocalizationValue(catalog.NameId));

      IProductProvider productProvider = StringActivator.CreateInstance<IProductProvider>(catalog.CSharpClassName);

      if (productProvider != null)
        expandoObjectBuilder.AddProperty(
          "Products",
          productProvider.GetProducts(this.requestHandler, catalog).Select(p => this.CreateProductViewModel(p)).ToList()
        );

      return expandoObjectBuilder.Build();
    }

    private dynamic CreateProductViewModel(Product product)
    {
      Photo coverPhoto = this.requestHandler.Storage.GetRepository<IPhotoRepository>().CoverByProductId(product.Id);

      return new ExpandoObjectBuilder()
        .AddProperty("Id", product.Id)
        .AddProperty("Url", product.Url)
        .AddProperty("Code", product.Code)
        .AddProperty("Name", this.requestHandler.GetLocalizationValue(product.NameId))
        .AddProperty("Price", product.Price)
        .AddProperty("CoverPhoto", this.CreatePhotoViewModel(coverPhoto))
        .Build();
    }

    private dynamic CreatePhotoViewModel(Photo photo)
    {
      if (photo == null)
        return null;

      return new ExpandoObjectBuilder()
        .AddProperty("Filename", photo.Filename)
        .Build();
    }
  }
}