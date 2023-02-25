// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.Metadata;

namespace Platformus.Website.Backend.Metadata;

public class Metadata : MetadataBase
{
  public override IEnumerable<StyleSheet> GetStyleSheets(HttpContext httpContext)
  {
    return new StyleSheet[]
    {
      new StyleSheet("/wwwroot.areas.backend.css.platformus.website.min.css", 1000)
    };
  }

  public override IEnumerable<Script> GetScripts(HttpContext httpContext)
  {
    return new Script[]
    {
      new Script("/wwwroot.areas.backend.js.platformus.website.min.js", 2010)
    };
  }

  public override IEnumerable<MenuGroup> GetMenuGroups(HttpContext httpContext)
  {
    IStringLocalizer<Metadata> localizer = httpContext.GetStringLocalizer<Metadata>();

    return new MenuGroup[]
    {
      new MenuGroup(
        localizer["Content"],
        1000,
        new MenuItem[]
        {
          new MenuItem("icon--objects", "/backend/objects", localizer["Objects"], 1000, Permissions.ManageObjects),
          new MenuItem("icon--menus", "/backend/menus", localizer["Menus"], 2000, Permissions.ManageMenus),
          new MenuItem("icon--forms", "/backend/forms", localizer["Forms"], 3000, Permissions.ManageForms),
          new MenuItem("icon--file-manager", "/backend/filemanager", localizer["File manager"], 4000, Permissions.ManageFileManager)
        }
      ),
      new MenuGroup(
        localizer["Development"],
        5000,
        new MenuItem[]
        {
          new MenuItem("icon--classes", "/backend/classes", localizer["Classes"], 1000, Permissions.ManageClasses),
          new MenuItem("icon--endpoints", "/backend/endpoints", localizer["Endpoints"], 2000, Permissions.ManageEndpoints)
        }
      )
    };
  }
}