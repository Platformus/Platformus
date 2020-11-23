// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Core.Extensions;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;

namespace Platformus.Website.EventHandlers
{
  public class ObjectCreatedEventHandler : IObjectCreatedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(HttpContext httpContext, Object @object)
    {
      httpContext.GetCache().RemoveAll();
    }
  }
}