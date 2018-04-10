// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Orders
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take, string filter)
    {
      IOrderRepository orderRepository = this.RequestHandler.Storage.GetRepository<IOrderRepository>();
      IStringLocalizer<IndexViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, orderRepository.Count(filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Identifier"], "Id"),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Customer"]),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Order state"]),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Payment method"]),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Delivery method"]),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Total"]),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Created"], "Created"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          orderRepository.Range(orderBy, direction, skip, take, filter).ToList().Select(o => new OrderViewModelFactory(this.RequestHandler).Create(o)),
          "_Order"
        )
      };
    }
  }
}