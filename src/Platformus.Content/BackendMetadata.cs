// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Infrastructure;

namespace Platformus.Content
{
  public class BackendMetadata : BackendMetadataBase
  {
    public override IEnumerable<BackendStyleSheet> BackendStyleSheets
    {
      get
      {
        return new BackendStyleSheet[]
        {
          new BackendStyleSheet("/wwwroot.areas.backend.css.platformus.content.min.css", 2000)
        };
      }
    }

    public override IEnumerable<BackendScript> BackendScripts
    {
      get
      {
        return new BackendScript[]
        {
          new BackendScript("/wwwroot.areas.backend.js.platformus.content.min.js", 2000)
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
            "Content",
            1000,
            new BackendMenuItem[]
            {
              new BackendMenuItem("/backend/objects", "Objects", 1000)
            }
          ),
          new BackendMenuGroup(
            "Settings",
            2000,
            new BackendMenuItem[]
            {
              new BackendMenuItem("/backend/datatypes", "Data types", 2000),
              new BackendMenuItem("/backend/classes", "Classes", 3000)
            }
          )
        };
      }
    }
  }
}