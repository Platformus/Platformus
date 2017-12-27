// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Microsoft.Extensions.Localization;

namespace Platformus.ECommerce.Backend.ViewModels.Products
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take, string filter)
    {
      IProductRepository productRepository = this.RequestHandler.Storage.GetRepository<IProductRepository>();
      IStringLocalizer<IndexViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, productRepository.Count(filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Code"], "Code"),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          productRepository.Range(orderBy, direction, skip, take, filter).ToList().Select(p => new ProductViewModelFactory(this.RequestHandler).Create(p)),
          "_Product"
        )
      };
    }
  }
}