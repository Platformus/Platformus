// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class CartViewModelFactory : ViewModelFactoryBase
  {
    public CartViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CartViewModel Create(Cart cart, bool loadPositions = false)
    {
      IEnumerable<Position> positions =
        loadPositions ? this.RequestHandler.Storage.GetRepository<IPositionRepository>().FilteredByCartId(cart.Id).ToList() : null;

      return new CartViewModel()
      {
        Id = cart.Id,
        Total = cart.GetTotal(this.RequestHandler),
        Created = cart.Created,
        Positions = positions == null ? null : positions.Select(
          p => new PositionViewModelFactory(this.RequestHandler).Create(p)
        )
      };
    }
  }
}