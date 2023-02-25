// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.OrderStates;

public static class CreateOrEditViewModelFactory
{
  public static CreateOrEditViewModel Create(HttpContext httpContext, OrderState orderState)
  {
    if (orderState == null)
      return new CreateOrEditViewModel()
      {
        NameLocalizations = httpContext.GetLocalizations()
      };

    return new CreateOrEditViewModel()
    {
      Id = orderState.Id,
      Code = orderState.Code,
      NameLocalizations = httpContext.GetLocalizations(orderState.Name),
      Position = orderState.Position
    };
  }
}