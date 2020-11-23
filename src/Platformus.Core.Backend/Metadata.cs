// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Platformus.Core.Backend.Metadata
{
  public class Metadata : MetadataBase
  {
    public override IEnumerable<StyleSheet> GetStyleSheets(HttpContext httpContext)
    {
      return new StyleSheet[]
      {
        new StyleSheet("/wwwroot.areas.backend.css.platformus.core.min.css", 1000),
        new StyleSheet("//fonts.googleapis.com/css?family=PT+Sans:400,400italic&subset=latin,cyrillic", 10000)
      };
    }

    public override IEnumerable<Script> GetScripts(HttpContext httpContext)
    {
      return new Script[]
      {
        new Script("//ajax.aspnetcdn.com/ajax/jquery/jquery-1.11.3.min.js", 100),
        new Script("//ajax.aspnetcdn.com/ajax/jquery.validate/1.14.0/jquery.validate.min.js", 200),
        new Script("//ajax.aspnetcdn.com/ajax/jquery.validation.unobtrusive/3.2.6/jquery.validate.unobtrusive.min.js", 300),
        new Script("//cdnjs.cloudflare.com/ajax/libs/jquery-cookie/1.4.1/jquery.cookie.min.js", 400),
        new Script("//cdn.tinymce.com/4/tinymce.min.js", 500),
        new Script("//cdnjs.cloudflare.com/ajax/libs/moment.js/2.17.1/moment-with-locales.min.js", 600),
        new Script("/wwwroot.areas.backend.js.platformus.core.min.js", 1000)
      };
    }

    public override IEnumerable<MenuGroup> GetMenuGroups(HttpContext httpContext)
    {
      IStringLocalizer<Metadata> localizer = httpContext.RequestServices.GetService<IStringLocalizer<Metadata>>();

      return new MenuGroup[]
      {
        new MenuGroup(
          localizer["Audience"],
          2000,
          new MenuItem[]
          {
            new MenuItem("/backend/permissions", localizer["Permissions"], 1000, new string[] { Permissions.ManagePermissions }),
            new MenuItem("/backend/roles", localizer["Roles"], 2000, new string[] { Permissions.ManageRoles }),
            new MenuItem("/backend/users", localizer["Users"], 3000, new string[] { Permissions.ManageUsers })
          }
        ),
        new MenuGroup(
          localizer["Administration"],
          3000,
          new MenuItem[]
          {
            new MenuItem("/backend/configurations", localizer["Configurations"], 1000, new string[] { Permissions.ManageConfigurations }),
            new MenuItem("/backend/cultures", localizer["Cultures"], 2000, new string[] { Permissions.ManageCultures })
          }
        )
      };
    }
  }
}