// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace Platformus.Core.Backend.Metadata;

public class Metadata : MetadataBase
{
  public override IEnumerable<StyleSheet> GetStyleSheets(HttpContext httpContext)
  {
    return new StyleSheet[]
    {
      new StyleSheet("/wwwroot.areas.backend.css.platformus.core.min.css", 1000),
      new StyleSheet("https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;700&display=swap", 10000)
    };
  }

  public override IEnumerable<Script> GetScripts(HttpContext httpContext)
  {
    return new Script[]
    {
      new Script("//cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js", 100),
      new Script("//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.3/jquery.validate.min.js", 200),
      new Script("//cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.12/jquery.validate.unobtrusive.min.js", 300),
      new Script("//cdnjs.cloudflare.com/ajax/libs/jquery-cookie/1.4.1/jquery.cookie.min.js", 400),
      new Script("//cdnjs.cloudflare.com/ajax/libs/tinymce/4.9.11/tinymce.min.js", 500),
      new Script("//cdnjs.cloudflare.com/ajax/libs/jquery.maskedinput/1.4.1/jquery.maskedinput.min.js", 600),
      new Script("//cdnjs.cloudflare.com/ajax/libs/ace/1.4.13/ace.js", 700),
      new Script("//cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment-with-locales.min.js", 800),
      new Script("/wwwroot.areas.backend.js.platformus.core.min.js", 1000)
    };
  }

  public override IEnumerable<MenuGroup> GetMenuGroups(HttpContext httpContext)
  {
    IStringLocalizer<Metadata> localizer = httpContext.GetStringLocalizer<Metadata>();

    return new MenuGroup[]
    {
      new MenuGroup(
        localizer["Audience"],
        3000,
        new MenuItem[]
        {
          new MenuItem("icon--permissions", "/backend/permissions", localizer["Permissions"], 1000, Permissions.ManagePermissions),
          new MenuItem("icon--roles", "/backend/roles", localizer["Roles"], 2000, Permissions.ManageRoles),
          new MenuItem("icon--users", "/backend/users", localizer["Users"], 3000, Permissions.ManageUsers)
        }
      ),
      new MenuGroup(
        localizer["Administration"],
        4000,
        new MenuItem[]
        {
          new MenuItem("icon--configurations", "/backend/configurations", localizer["Configurations"], 1000, Permissions.ManageConfigurations),
          new MenuItem("icon--cultures", "/backend/cultures", localizer["Cultures"], 2000, Permissions.ManageCultures)
        }
      )
    };
  }
}