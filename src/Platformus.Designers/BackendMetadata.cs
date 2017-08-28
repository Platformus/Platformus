// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Infrastructure;

namespace Platformus.Designers
{
  public class BackendMetadata : BackendMetadataBase
  {
    public override IEnumerable<BackendStyleSheet> BackendStyleSheets
    {
      get
      {
        return new BackendStyleSheet[]
        {
          new BackendStyleSheet("/wwwroot.areas.backend.css.platformus.designers.min.css", 10000)
        };
      }
    }

    public override IEnumerable<BackendScript> BackendScripts
    {
      get
      {
        return new BackendScript[]
        {
          new BackendScript("//cdnjs.cloudflare.com/ajax/libs/ace/1.2.6/ace.js", 10000)
        };
      }
    }

    public override IEnumerable<BackendMenuGroup> GetBackendMenuGroups(IServiceProvider serviceProvider)
    {
      IStringLocalizer<BackendMetadata> localizer = serviceProvider.GetService<IStringLocalizer<BackendMetadata>>();

      return new BackendMenuGroup[]
      {
        new BackendMenuGroup(
          localizer["Development"],
          4000,
          new BackendMenuItem[]
          {
            new BackendMenuItem("/backend/views", localizer["Views"], 10000, new string[] { Permissions.BrowseViews }),
            new BackendMenuItem("/backend/styles", localizer["Styles"], 11000, new string[] { Permissions.BrowseStyles }),
            new BackendMenuItem("/backend/scripts", localizer["Scripts"], 12000, new string[] { Permissions.BrowseScripts }),
            new BackendMenuItem("/backend/bundles", localizer["Bundles"], 13000, new string[] { Permissions.BrowseBundles })
          }
        )
      };
    }
  }
}