// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class AttributeViewModelFactory : ViewModelFactoryBase
  {
    public AttributeViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public AttributeViewModel Create(SerializedAttribute serializedAttribute)
    {
      return new AttributeViewModel()
      {
        Id = serializedAttribute.AttributeId,
        Value = serializedAttribute.Value
      };
    }
  }
}