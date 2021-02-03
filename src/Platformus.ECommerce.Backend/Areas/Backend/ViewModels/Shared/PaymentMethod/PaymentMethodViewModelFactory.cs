// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class PaymentMethodViewModelFactory : ViewModelFactoryBase
  {
    public PaymentMethodViewModel Create(PaymentMethod paymentMethod)
    {
      return new PaymentMethodViewModel()
      {
        Id = paymentMethod.Id,
        Name = paymentMethod.Name.GetLocalizationValue(),
        Position = paymentMethod.Position
      };
    }
  }
}