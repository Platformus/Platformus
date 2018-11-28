// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
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
      SerializedProduct serializedProduct = this.requestHandler.Storage.GetRepository<ISerializedProductRepository>().WithUrl(url);

      if (serializedProduct == null)
        throw new HttpException(404, "Not found.");

      IEnumerable<SerializedProduct.Attribute> serializedAttributes = JsonConvert.DeserializeObject<IEnumerable<SerializedProduct.Attribute>>(serializedProduct.SerializedAttributes);
      IEnumerable<SerializedProduct.Photo> serializedPhotos = JsonConvert.DeserializeObject<IEnumerable<SerializedProduct.Photo>>(serializedProduct.SerializedPhotos);
      SerializedProduct.Photo serializedCoverPhoto = serializedPhotos.FirstOrDefault(sph => sph.IsCover);

      return new ExpandoObjectBuilder()
        .AddProperty("Id", serializedProduct.ProductId)
        .AddProperty("Code", serializedProduct.Code)
        .AddProperty("Name", serializedProduct.Name)
        .AddProperty("Description", serializedProduct.Description)
        .AddProperty("Price", serializedProduct.Price)
        .AddProperty("Title", serializedProduct.Title)
        .AddProperty("MetaDescription", serializedProduct.MetaDescription)
        .AddProperty("MetaKeywords", serializedProduct.MetaKeywords)
        .AddProperty("Attributes", serializedAttributes.OrderBy(sa => sa.Feature.Position).Select(sa => this.CreateAttributeViewModel(sa)))
        .AddProperty("CoverPhoto", this.CreatePhotoViewModel(serializedCoverPhoto))
        .AddProperty("Photos", serializedPhotos.OrderBy(sph => sph.Position).Select(sph => this.CreatePhotoViewModel(sph)))
        .Build();
    }

    private dynamic CreateAttributeViewModel(SerializedProduct.Attribute serializedAttribute)
    {
      if (serializedAttribute == null)
        return null;

      return new ExpandoObjectBuilder()
        .AddProperty("Feature", this.CreateFeatureViewModel(serializedAttribute.Feature))
        .AddProperty("Value", serializedAttribute.Value)
        .Build();
    }

    private dynamic CreateFeatureViewModel(SerializedProduct.Feature serializedFeature)
    {
      if (serializedFeature == null)
        return null;

      return new ExpandoObjectBuilder()
        .AddProperty("Code", serializedFeature.Code)
        .AddProperty("Name", serializedFeature.Name)
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