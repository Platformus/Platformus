// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.PaymentMethods
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public PaymentMethod Map(CreateOrEditViewModel createOrEdit)
    {
      PaymentMethod paymentMethod = new PaymentMethod();

      if (createOrEdit.Id != null)
        paymentMethod = this.RequestHandler.Storage.GetRepository<IPaymentMethodRepository>().WithKey((int)createOrEdit.Id);

      paymentMethod.Code = createOrEdit.Code;
      paymentMethod.Position = createOrEdit.Position;
      return paymentMethod;
    }
  }
}