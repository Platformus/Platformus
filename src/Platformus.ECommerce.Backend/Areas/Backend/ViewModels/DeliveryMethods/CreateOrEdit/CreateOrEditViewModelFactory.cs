// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.DeliveryMethods
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = this.GetLocalizations()
        };

      DeliveryMethod deliveryMethod = this.RequestHandler.Storage.GetRepository<IDeliveryMethodRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = deliveryMethod.Id,
        Code = deliveryMethod.Code,
        NameLocalizations = this.GetLocalizations(deliveryMethod.NameId),
        Position = deliveryMethod.Position
      };
    }
  }
}