// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class FeatureViewModelFactory : ViewModelFactoryBase
  {
    public FeatureViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public FeatureViewModel Create(Feature feature)
    {
      return new FeatureViewModel()
      {
        Id = feature.Id,
        Code = feature.Code,
        Name = this.GetLocalizationValue(feature.NameId),
        Position = feature.Position
      };
    }
  }
}