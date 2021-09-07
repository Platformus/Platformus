// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Website.Events;

namespace Platformus.Website.EventHandlers
{
  public class EndpointEditedEventHandler : IEndpointEditedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(HttpContext httpContext, Data.Entities.Endpoint oldEndpoint, Data.Entities.Endpoint newEndpoint)
    {
      httpContext.GetCache().RemoveAll();
      ResponseCacheManager.RemoveAll(httpContext);
    }
  }
}