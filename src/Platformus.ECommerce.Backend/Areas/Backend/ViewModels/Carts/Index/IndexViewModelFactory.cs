// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Carts
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(string cartBy, string direction, int skip, int take, string filter)
    {
      ICartRepository cartRepository = this.RequestHandler.Storage.GetRepository<ICartRepository>();
      IStringLocalizer<IndexViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          cartBy, direction, skip, take, cartRepository.Count(filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Identifier"], "Id"),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Total"]),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Created"], "Created"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          cartRepository.Range(cartBy, direction, skip, take, filter).ToList().Select(c => new CartViewModelFactory(this.RequestHandler).Create(c)),
          "_Cart"
        )
      };
    }
  }
}