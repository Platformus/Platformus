﻿// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Platformus.Core;
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
      {
        if (string.IsNullOrEmpty(this.requestHandler.HttpContext.Request.Query["attributeids"]))
          expandoObjectBuilder.AddProperty(
            "Products",
            productProvider.GetProducts(this.requestHandler, catalog).Select(sp => this.CreateProductViewModel(sp)).ToList()
          );

        else expandoObjectBuilder.AddProperty(
          "Products",
          productProvider.GetProducts(this.requestHandler, catalog, this.requestHandler.HttpContext.Request.Query["attributeids"].ToString().Split(',').Select(id => int.Parse(id)).ToArray()).Select(sp => this.CreateProductViewModel(sp)).ToList()
        );
      }

      return expandoObjectBuilder.Build();
    }

    private dynamic CreateProductViewModel(SerializedProduct serializedProduct)
    {
      IEnumerable<SerializedProduct.Photo> serializedPhotos = JsonConvert.DeserializeObject<IEnumerable<SerializedProduct.Photo>>(serializedProduct.SerializedPhotos);
      SerializedProduct.Photo serializedCoverPhoto = serializedPhotos.FirstOrDefault(sph => sph.IsCover);

      return new ExpandoObjectBuilder()
        .AddProperty("Id", serializedProduct.ProductId)
        .AddProperty("Url", serializedProduct.Url)
        .AddProperty("Code", serializedProduct.Code)
        .AddProperty("Name", serializedProduct.Name)
        .AddProperty("Price", serializedProduct.Price)
        .AddProperty("CoverPhoto", this.CreatePhotoViewModel(serializedCoverPhoto))
        .Build();
    }

    private dynamic CreatePhotoViewModel(SerializedProduct.Photo serializedPhoto)
    {
      if (serializedPhoto == null)
        return null;

      return new ExpandoObjectBuilder()
        .AddProperty("Filename", serializedPhoto.Filename)
        .Build();
    }
  }
}