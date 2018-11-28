// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization;
using Platformus.Globalization.Data.Entities;

namespace Platformus.ECommerce
{
  public class ProductSerializationManager
  {
    public IRequestHandler requestHandler;

    public ProductSerializationManager(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
    }

    public void SerializeProduct(Product product)
    {
      foreach (Culture culture in this.requestHandler.GetService<ICultureManager>().GetNotNeutralCultures())
      {
        SerializedProduct serializedProduct = this.requestHandler.Storage.GetRepository<ISerializedProductRepository>().WithKey(culture.Id, product.Id);

        if (serializedProduct == null)
          this.requestHandler.Storage.GetRepository<ISerializedProductRepository>().Create(this.SerializeProduct(culture, product));

        else
        {
          SerializedProduct temp = this.SerializeProduct(culture, product);

          serializedProduct.CategoryId = temp.CategoryId;
          serializedProduct.Url = temp.Url;
          serializedProduct.Code = temp.Code;
          serializedProduct.Name = temp.Name;
          serializedProduct.Description = temp.Description;
          serializedProduct.Title = temp.Title;
          serializedProduct.Price = temp.Price;
          serializedProduct.MetaDescription = temp.MetaDescription;
          serializedProduct.MetaKeywords = temp.MetaKeywords;
          serializedProduct.SerializedAttributes = temp.SerializedAttributes;
          serializedProduct.SerializedPhotos = temp.SerializedPhotos;
          this.requestHandler.Storage.GetRepository<ISerializedProductRepository>().Edit(serializedProduct);
        }
      }

      this.requestHandler.Storage.Save();
    }

    private SerializedProduct SerializeProduct(Culture culture, Product product)
    {
      List<SerializedProduct.Attribute> serializedAttributes = new List<SerializedProduct.Attribute>();

      foreach (ProductAttribute productAttribute in this.requestHandler.Storage.GetRepository<IProductAttributeRepository>().FilteredByProductId(product.Id))
      {
        Attribute attribute = this.requestHandler.Storage.GetRepository<IAttributeRepository>().WithKey(productAttribute.AttributeId);

        serializedAttributes.Add(this.SerializeAttribute(culture, attribute));
      }

      List<SerializedProduct.Photo> serializedPhotos = new List<SerializedProduct.Photo>();

      foreach (Photo photo in this.requestHandler.Storage.GetRepository<IPhotoRepository>().FilteredByProductId(product.Id))
        serializedPhotos.Add(this.SerializePhoto(culture, photo));

      SerializedProduct serializedProduct = new SerializedProduct();

      serializedProduct.CultureId = culture.Id;
      serializedProduct.ProductId = product.Id;
      serializedProduct.CategoryId = product.CategoryId;
      serializedProduct.Url = product.Url;
      serializedProduct.Code = product.Code;
      serializedProduct.Name = this.requestHandler.GetLocalizationValue(product.NameId);
      serializedProduct.Description = this.requestHandler.GetLocalizationValue(product.DescriptionId);
      serializedProduct.Price = product.Price;
      serializedProduct.Title = this.requestHandler.GetLocalizationValue(product.TitleId);
      serializedProduct.MetaDescription = this.requestHandler.GetLocalizationValue(product.MetaDescriptionId);
      serializedProduct.MetaKeywords = this.requestHandler.GetLocalizationValue(product.MetaKeywordsId);

      if (serializedAttributes.Count != 0)
        serializedProduct.SerializedAttributes = this.SerializeObject(serializedAttributes);

      if (serializedPhotos.Count != 0)
        serializedProduct.SerializedPhotos = this.SerializeObject(serializedPhotos);

      return serializedProduct;
    }

    private SerializedProduct.Attribute SerializeAttribute(Culture culture, Attribute attribute)
    {
      Feature feature = this.requestHandler.Storage.GetRepository<IFeatureRepository>().WithKey(attribute.FeatureId);
      SerializedProduct.Attribute serializedAttribute = new SerializedProduct.Attribute();

      serializedAttribute.Feature = this.SerializeFeature(culture, feature);
      serializedAttribute.Value = this.requestHandler.GetLocalizationValue(attribute.ValueId);
      serializedAttribute.Position = attribute.Position;
      return serializedAttribute;
    }

    private SerializedProduct.Feature SerializeFeature(Culture culture, Feature feature)
    {
      SerializedProduct.Feature serializedFeature = new SerializedProduct.Feature();

      serializedFeature.Code = feature.Code;
      serializedFeature.Name = this.requestHandler.GetLocalizationValue(feature.NameId);
      serializedFeature.Position = feature.Position;
      return serializedFeature;
    }

    private SerializedProduct.Photo SerializePhoto(Culture culture, Photo photo)
    {
      SerializedProduct.Photo serializedPhoto = new SerializedProduct.Photo();

      serializedPhoto.Filename = photo.Filename;
      serializedPhoto.IsCover = photo.IsCover;
      serializedPhoto.Position = photo.Position;
      return serializedPhoto;
    }

    private string SerializeObject(object value)
    {
      string result = JsonConvert.SerializeObject(value);

      if (string.IsNullOrEmpty(result))
        return null;

      return result;
    }
  }
}