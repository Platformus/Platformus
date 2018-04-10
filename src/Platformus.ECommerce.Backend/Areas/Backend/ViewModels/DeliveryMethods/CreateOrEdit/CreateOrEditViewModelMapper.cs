// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.DeliveryMethods
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public DeliveryMethod Map(CreateOrEditViewModel createOrEdit)
    {
      DeliveryMethod deliveryMethod = new DeliveryMethod();

      if (createOrEdit.Id != null)
        deliveryMethod = this.RequestHandler.Storage.GetRepository<IDeliveryMethodRepository>().WithKey((int)createOrEdit.Id);

      deliveryMethod.Code = createOrEdit.Code;
      deliveryMethod.Position = createOrEdit.Position;
      return deliveryMethod;
    }
  }
}