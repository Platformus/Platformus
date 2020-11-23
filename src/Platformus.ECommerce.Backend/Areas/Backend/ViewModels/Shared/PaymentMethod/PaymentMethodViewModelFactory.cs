// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core;
using Platformus.ECommerce.Data.Entities;
using Platformus.Core.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class PaymentMethodViewModelFactory : ViewModelFactoryBase
  {
    public PaymentMethodViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public PaymentMethodViewModel Create(PaymentMethod paymentMethod)
    {
      return new PaymentMethodViewModel()
      {
        Id = paymentMethod.Id,
        Code = paymentMethod.Code,
        Name = this.GetLocalizationValue(paymentMethod.NameId),
        Position = paymentMethod.Position
      };
    }
  }
}