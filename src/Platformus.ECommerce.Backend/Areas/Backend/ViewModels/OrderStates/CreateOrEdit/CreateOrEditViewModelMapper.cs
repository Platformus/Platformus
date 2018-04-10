// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.OrderStates
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public OrderState Map(CreateOrEditViewModel createOrEdit)
    {
      OrderState orderState = new OrderState();

      if (createOrEdit.Id != null)
        orderState = this.RequestHandler.Storage.GetRepository<IOrderStateRepository>().WithKey((int)createOrEdit.Id);

      orderState.Code = createOrEdit.Code;
      orderState.Position = createOrEdit.Position;
      return orderState;
    }
  }
}