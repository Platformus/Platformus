// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Globalization;
using Platformus.Globalization.Data.Models;
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
            string defaultCultureCode = "en";
            IStorage storage = this.serviceProvider.GetService<IStorage>();

            if (storage != null)
            {
              Culture defaultCulture = CultureManager.GetDefaultCulture(storage);

              if (defaultCulture != null)
                defaultCultureCode = defaultCulture.Code;
            }

            routeBuilder.MapRoute(
              name: "Default",
              template: "{culture=" + defaultCultureCode + "}/{*url}",
              defaults: new { controller = "Default", action = "Index" }
            );
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