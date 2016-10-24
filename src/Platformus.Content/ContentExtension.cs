// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Platformus.Infrastructure;

namespace Platformus.Content
{
  public class ContentExtension : ExtensionBase
  {
    public override IEnumerable<KeyValuePair<int, Action<IRouteBuilder>>> UseMvcActionsByPriorities
    {
      get
      {
        return new Dictionary<int, Action<IRouteBuilder>>()
        {
          [10000] = routeBuilder =>
          {
            routeBuilder.MapRoute(name: "DefaultWithCulture", template: "{culture=en}/{*url}", defaults: new { controller = "Default", action = "Index" });
            routeBuilder.MapRoute(name: "Default", template: "{*url}", defaults: new { controller = "Default", action = "Index" });
          }
        };
      }
    }

    public override IBackendMetadata BackendMetadata
    {
      get
      {
        return new BackendMetadata();
      }
    }
  }
}
