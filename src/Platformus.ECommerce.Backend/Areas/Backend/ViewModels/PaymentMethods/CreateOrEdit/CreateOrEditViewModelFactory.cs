// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Core.Extensions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.PaymentMethods
{
  public static class CreateOrEditViewModelFactory
  {
    public static CreateOrEditViewModel Create(HttpContext httpContext, PaymentMethod paymentMethod)
    {
      if (paymentMethod == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = httpContext.GetLocalizations()
        };

      return new CreateOrEditViewModel()
      {
        Id = paymentMethod.Id,
        Code = paymentMethod.Code,
        NameLocalizations = httpContext.GetLocalizations(paymentMethod.Name),
        Position = paymentMethod.Position
      };
    }
  }
}