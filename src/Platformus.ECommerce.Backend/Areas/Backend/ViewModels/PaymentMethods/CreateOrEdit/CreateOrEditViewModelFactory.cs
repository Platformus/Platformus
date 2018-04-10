// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.PaymentMethods
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

      PaymentMethod paymentMethod = this.RequestHandler.Storage.GetRepository<IPaymentMethodRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = paymentMethod.Id,
        Code = paymentMethod.Code,
        NameLocalizations = this.GetLocalizations(paymentMethod.NameId),
        Position = paymentMethod.Position
      };
    }
  }
}