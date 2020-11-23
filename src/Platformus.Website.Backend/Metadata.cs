// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.Metadata;

namespace Platformus.Website.Backend.Metadata
{
  public class Metadata : MetadataBase
  {
    public override IEnumerable<Script> GetScripts(HttpContext httpContext)
    {
      return new Script[]
      {
        new Script("/wwwroot.areas.backend.js.platformus.website.min.js", 2000)
      };
    }

    public override IEnumerable<MenuGroup> GetMenuGroups(HttpContext httpContext)
    {
      IStringLocalizer<Metadata> localizer = httpContext.RequestServices.GetService<IStringLocalizer<Metadata>>();

      return new MenuGroup[]
      {
        new MenuGroup(
          localizer["Content"],
          1000,
          new MenuItem[]
          {
            new MenuItem("/backend/objects", localizer["Objects"], 1000, new string[] { Permissions.ManageObjects }),
            new MenuItem("/backend/menus", localizer["Menus"], 2000, new string[] { Permissions.ManageMenus }),
            new MenuItem("/backend/forms", localizer["Forms"], 3000, new string[] { Permissions.ManageForms }),
            new MenuItem("/backend/filemanager", localizer["File manager"], 4000, new string[] { Permissions.ManageFileManager })
          }
        ),
        new MenuGroup(
          localizer["Development"],
          4000,
          new MenuItem[]
          {
            new MenuItem("/backend/datatypes", localizer["Data types"], 1000, new string[] { Permissions.ManageDataTypes }),
            new MenuItem("/backend/classes", localizer["Classes"], 2000, new string[] { Permissions.ManageClasses }),
            new MenuItem("/backend/endpoints", localizer["Endpoints"], 3000, new string[] { Permissions.ManageEndpoints })
          }
        )
      };
    }
  }
}