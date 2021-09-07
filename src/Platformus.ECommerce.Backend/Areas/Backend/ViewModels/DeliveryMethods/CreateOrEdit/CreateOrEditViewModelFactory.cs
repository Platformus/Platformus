// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.DeliveryMethods
{
  public static class CreateOrEditViewModelFactory
  {
    public static CreateOrEditViewModel Create(HttpContext httpContext, DeliveryMethod deliveryMethod)
    {
      if (deliveryMethod == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = httpContext.GetLocalizations()
        };

      return new CreateOrEditViewModel()
      {
        Id = deliveryMethod.Id,
        Code = deliveryMethod.Code,
        NameLocalizations = httpContext.GetLocalizations(deliveryMethod.Name),
        Position = deliveryMethod.Position
      };
    }
  }
}