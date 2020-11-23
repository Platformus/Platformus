// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Extensions;

namespace Platformus.Core.EventHandlers
{
  public class CultureCreatedEventHandler : ICultureCreatedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(HttpContext httpContext, Culture culture)
    {
      httpContext.GetCultureManager().InvalidateCache();
    }
  }
}