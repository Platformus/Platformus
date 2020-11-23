// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core;
using Platformus.ECommerce.Data.Entities;
using Platformus.Core.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class FeatureViewModelFactory : ViewModelFactoryBase
  {
    public FeatureViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public FeatureViewModel Create(SerializedAttribute.Feature serializedFeature)
    {
      return new FeatureViewModel()
      {
        Code = serializedFeature.Code,
        Name = serializedFeature.Name
      };
    }
  }
}