// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
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

    public override IEnumerable<BackendMenuGroup> BackendMenuGroups
    {
      get
      {
        return new BackendMenuGroup[]
        {
          new BackendMenuGroup(
            "Development",
            4000,
            new BackendMenuItem[]
            {
              new BackendMenuItem("/backend/views", "Views", 10000, new string[] { Permissions.BrowseViews }),
              new BackendMenuItem("/backend/styles", "Styles", 11000, new string[] { Permissions.BrowseStyles }),
              new BackendMenuItem("/backend/scripts", "Scripts", 12000, new string[] { Permissions.BrowseScripts }),
              new BackendMenuItem("/backend/bundles", "Bundles", 13000, new string[] { Permissions.BrowseBundles })
            }
          )
        };
      }
    }
  }
}