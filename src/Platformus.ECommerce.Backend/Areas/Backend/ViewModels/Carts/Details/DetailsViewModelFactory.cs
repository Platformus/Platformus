// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Carts
{
  public class DetailsViewModelFactory : ViewModelFactoryBase
  {
    public DetailsViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public DetailsViewModel Create(int id)
    {
      Cart cart = this.RequestHandler.Storage.GetRepository<ICartRepository>().WithKey(id);

      return new DetailsViewModel()
      {
        Id = cart.Id,
        ClientSideId = cart.ClientSideId,
        Cart = new CartViewModelFactory(this.RequestHandler).Create(cart, true)
      };
    }
  }
}