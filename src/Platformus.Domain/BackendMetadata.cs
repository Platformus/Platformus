// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Infrastructure;

namespace Platformus.Domain
{
  public class BackendMetadata : BackendMetadataBase
  {
    public override IEnumerable<BackendStyleSheet> BackendStyleSheets
    {
      get
      {
        return new BackendStyleSheet[]
        {
          new BackendStyleSheet("/wwwroot.areas.backend.css.platformus.domain.min.css", 2000)
        };
      }
    }

    public override IEnumerable<BackendScript> BackendScripts
    {
      get
      {
        return new BackendScript[]
        {
          new BackendScript("/wwwroot.areas.backend.js.platformus.domain.min.js", 2000)
        };
      }
    }

    public override IEnumerable<BackendMenuGroup> GetBackendMenuGroups(IServiceProvider serviceProvider)
    {
      IStringLocalizer<BackendMetadata> localizer = serviceProvider.GetService<IStringLocalizer<BackendMetadata>>();

      return new BackendMenuGroup[]
      {
        new BackendMenuGroup(
          localizer["Content"],
          1000,
          new BackendMenuItem[]
          {
            new BackendMenuItem("/backend/objects", localizer["Objects"], 1000, new string[] { Permissions.BrowseObjects })
          }
        ),
        new BackendMenuGroup(
          localizer["Administration"],
          3000,
          new BackendMenuItem[]
          {
            new BackendMenuItem("/backend/datatypes", localizer["Data types"], 2000, new string[] { Permissions.BrowseDataTypes }),
            new BackendMenuItem("/backend/classes", localizer["Classes"], 3000, new string[] { Permissions.BrowseClasses })
          }
        )
      };
    }
  }
}