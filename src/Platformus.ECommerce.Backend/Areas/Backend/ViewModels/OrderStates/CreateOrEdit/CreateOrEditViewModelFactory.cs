// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.OrderStates
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          NameLocalizations = this.GetLocalizations()
        };

      OrderState orderState = this.RequestHandler.Storage.GetRepository<IOrderStateRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = orderState.Id,
        Code = orderState.Code,
        NameLocalizations = this.GetLocalizations(orderState.NameId),
        Position = orderState.Position
      };
    }
  }
}