// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class CartViewModelFactory : ViewModelFactoryBase
  {
    public CartViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CartViewModel Create(string additionalCssClass)
    {
      return new CartViewModel()
      {
        Positions = new CartManager(this.RequestHandler).Positions.Select(
          p => new PositionViewModelFactory(this.RequestHandler).Create(p)
        ).ToList(),
        AdditionalCssClass = additionalCssClass
      };
    }
  }
}