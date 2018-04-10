// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class DeliveryMethodViewModelFactory : ViewModelFactoryBase
  {
    public DeliveryMethodViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public DeliveryMethodViewModel Create(DeliveryMethod deliveryMethod)
    {
      return new DeliveryMethodViewModel()
      {
        Id = deliveryMethod.Id,
        Code = deliveryMethod.Code,
        Name = this.GetLocalizationValue(deliveryMethod.NameId),
        Position = deliveryMethod.Position
      };
    }
  }
}