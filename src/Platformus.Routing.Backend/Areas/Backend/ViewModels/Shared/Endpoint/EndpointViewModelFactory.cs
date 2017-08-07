// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Backend.ViewModels.Shared
{
  public class EndpointViewModelFactory : ViewModelFactoryBase
  {
    public EndpointViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public EndpointViewModel Create(Endpoint endpoint)
    {
      return new EndpointViewModel()
      {
        Id = endpoint.Id,
        Name = endpoint.Name,
        UrlTemplate = endpoint.UrlTemplate,
        Position = endpoint.Position
      };
    }
  }
}