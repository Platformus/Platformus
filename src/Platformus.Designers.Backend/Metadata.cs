// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.Metadata;

namespace Platformus.Designers.Backend.Metadata
{
  public class Metadata : MetadataBase
  {
    public override IEnumerable<StyleSheet> GetStyleSheets(IRequestHandler requestHandler)
    {
      return new StyleSheet[]
      {
        new StyleSheet("/wwwroot.areas.backend.css.platformus.designers.min.css", 10000)
      };
    }

    public override IEnumerable<Script> GetScripts(IRequestHandler requestHandler)
    {
      return new Script[]
      {
        new Script("//cdnjs.cloudflare.com/ajax/libs/ace/1.2.6/ace.js", 10000)
      };
    }

    public override IEnumerable<MenuGroup> GetMenuGroups(IRequestHandler requestHandler)
    {
      IStringLocalizer<Metadata> localizer = requestHandler.GetService<IStringLocalizer<Metadata>>();

      return new MenuGroup[]
      {
        new MenuGroup(
          localizer["Development"],
          4000,
          new MenuItem[]
          {
            new MenuItem("/backend/views", localizer["Views"], 10000, new string[] { Permissions.BrowseViews }),
            new MenuItem("/backend/styles", localizer["Styles"], 11000, new string[] { Permissions.BrowseStyles }),
            new MenuItem("/backend/scripts", localizer["Scripts"], 12000, new string[] { Permissions.BrowseScripts }),
            new MenuItem("/backend/bundles", localizer["Bundles"], 13000, new string[] { Permissions.BrowseBundles })
          }
        )
      };
    }
  }
}