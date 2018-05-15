// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class AttributeViewModelFactory : ViewModelFactoryBase
  {
    public AttributeViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public AttributeViewModel Create(Attribute attribute, bool loadFeature = false)
    {
      Feature feature = loadFeature ? this.RequestHandler.Storage.GetRepository<IFeatureRepository>().WithKey(attribute.FeatureId) : null;

      return new AttributeViewModel()
      {
        Id = attribute.Id,
        Feature = feature == null ? null : new FeatureViewModelFactory(this.RequestHandler).Create(feature),
        Value = this.GetLocalizationValue(attribute.ValueId),
        Position = attribute.Position
      };
    }
  }
}