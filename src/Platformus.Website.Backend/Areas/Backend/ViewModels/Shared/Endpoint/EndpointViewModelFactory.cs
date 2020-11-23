// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public class EndpointViewModelFactory : ViewModelFactoryBase
  {
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