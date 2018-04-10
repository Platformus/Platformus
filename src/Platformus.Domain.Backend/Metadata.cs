// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.Metadata;

namespace Platformus.Domain.Backend.Metadata
{
  public class Metadata : MetadataBase
  {
    public override IEnumerable<StyleSheet> GetStyleSheets(IRequestHandler requestHandler)
    {
      return new StyleSheet[]
      {
        new StyleSheet("/wwwroot.areas.backend.css.platformus.domain.min.css", 2000)
      };
    }

    public override IEnumerable<Script> GetScripts(IRequestHandler requestHandler)
    {
      return new Script[]
      {
        new Script("/wwwroot.areas.backend.js.platformus.domain.min.js", 2000)
      };
    }

    public override IEnumerable<MenuGroup> GetMenuGroups(IRequestHandler requestHandler)
    {
      IStringLocalizer<Metadata> localizer = requestHandler.GetService<IStringLocalizer<Metadata>>();

      return new MenuGroup[]
      {
        new MenuGroup(
          localizer["Content"],
          1000,
          new MenuItem[]
          {
            new MenuItem("/backend/objects", localizer["Objects"], 1000, new string[] { Permissions.BrowseObjects })
          }
        ),
        new MenuGroup(
          localizer["Administration"],
          3000,
          new MenuItem[]
          {
            new MenuItem("/backend/datatypes", localizer["Data types"], 2000, new string[] { Permissions.BrowseDataTypes }),
            new MenuItem("/backend/classes", localizer["Classes"], 3000, new string[] { Permissions.BrowseClasses })
          }
        )
      };
    }
  }
}