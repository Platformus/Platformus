// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Data.Entities;
using Platformus.Globalization.Services.Abstractions;

namespace Platformus.ECommerce
{
  public class AttributeSerializationManager
  {
    public IRequestHandler requestHandler;

    public AttributeSerializationManager(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
    }

    public void SerializeAttribute(Attribute attribute)
    {
      foreach (Culture culture in this.requestHandler.GetService<ICultureManager>().GetNotNeutralCultures())
      {
        SerializedAttribute serializedAttribute = this.requestHandler.Storage.GetRepository<ISerializedAttributeRepository>().WithKey(culture.Id, attribute.Id);

        if (serializedAttribute == null)
          this.requestHandler.Storage.GetRepository<ISerializedAttributeRepository>().Create(this.SerializeAttribute(culture, attribute));

        else
        {
          SerializedAttribute temp = this.SerializeAttribute(culture, attribute);

          serializedAttribute.Value = temp.Value;
          serializedAttribute.Position = temp.Position;
          serializedAttribute.SerializedFeature = temp.SerializedFeature;
          this.requestHandler.Storage.GetRepository<ISerializedAttributeRepository>().Edit(serializedAttribute);
        }
      }

      this.requestHandler.Storage.Save();
    }

    private SerializedAttribute SerializeAttribute(Culture culture, Attribute attribute)
    {
      SerializedAttribute.Feature serializedFeature = this.SerializeFeature(
        culture, this.requestHandler.Storage.GetRepository<IFeatureRepository>().WithKey(attribute.FeatureId)
      );

      SerializedAttribute serializedAttribute = new SerializedAttribute();

      serializedAttribute.CultureId = culture.Id;
      serializedAttribute.AttributeId = attribute.Id;
      serializedAttribute.Value = this.requestHandler.GetLocalizationValue(attribute.ValueId);
      serializedAttribute.Position = attribute.Position;
      serializedAttribute.SerializedFeature = this.SerializeObject(serializedFeature);
      return serializedAttribute;
    }

    private SerializedAttribute.Feature SerializeFeature(Culture culture, Feature feature)
    {
      SerializedAttribute.Feature serializedFeature = new SerializedAttribute.Feature();

      serializedFeature.Code = feature.Code;
      serializedFeature.Name = this.requestHandler.GetLocalizationValue(feature.NameId);
      serializedFeature.Position = feature.Position;
      return serializedFeature;
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