// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.OrderStates;

public static class CreateOrEditViewModelMapper
{
  public static OrderState Map(OrderState orderState, CreateOrEditViewModel createOrEdit)
  {
    orderState.Code = createOrEdit.Code;
    orderState.Position = createOrEdit.Position;
    return orderState;
  }
}