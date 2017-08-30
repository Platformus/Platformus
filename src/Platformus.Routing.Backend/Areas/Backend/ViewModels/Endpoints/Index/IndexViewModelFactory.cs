// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Routing.Backend.ViewModels.Shared;
using Platformus.Routing.Data.Abstractions;

namespace Platformus.Routing.Backend.ViewModels.Endpoints
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take, string filter)
    {
      IEndpointRepository endpointRepository = this.RequestHandler.Storage.GetRepository<IEndpointRepository>();
      IStringLocalizer<IndexViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, endpointRepository.Count(filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["URL template"], "UrlTemplate"),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Position"], "Position"),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Data sources"]),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          endpointRepository.Range(orderBy, direction, skip, take, filter).ToList().Select(m => new EndpointViewModelFactory(this.RequestHandler).Create(m)),
          "_Endpoint"
        )
      };
    }
  }
}