// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.PaymentMethods
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModel Create(HttpContext httpContext, PaymentMethod paymentMethod)
    {
      if (paymentMethod == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = this.GetLocalizations(httpContext)
        };

      return new CreateOrEditViewModel()
      {
        Id = paymentMethod.Id,
        Code = paymentMethod.Code,
        NameLocalizations = this.GetLocalizations(httpContext, paymentMethod.Name),
        Position = paymentMethod.Position
      };
    }
  }
}