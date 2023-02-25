// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Shared;

public static class OrderStateViewModelFactory
{
  public static OrderStateViewModel Create(OrderState orderState)
  {
    return new OrderStateViewModel()
    {
      Id = orderState.Id,
      Name = orderState.Name.GetLocalizationValue(),
      Position = orderState.Position
    };
  }
}