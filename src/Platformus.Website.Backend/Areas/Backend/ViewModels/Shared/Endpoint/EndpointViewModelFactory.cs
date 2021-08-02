// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public static class EndpointViewModelFactory
  {
    public static EndpointViewModel Create(Endpoint endpoint)
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