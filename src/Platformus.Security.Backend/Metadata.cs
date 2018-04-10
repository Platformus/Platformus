// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.Metadata;

namespace Platformus.Security.Backend.Metadata
{
  public class Metadata : MetadataBase
  {
    public override IEnumerable<MenuGroup> GetMenuGroups(IRequestHandler requestHandler)
    {
      IStringLocalizer<Metadata> localizer = requestHandler.GetService<IStringLocalizer<Metadata>>();

      return new MenuGroup[]
      {
        new MenuGroup(
          localizer["Audience"],
          2000,
          new MenuItem[]
          {
            new MenuItem("/backend/permissions", localizer["Permissions"], 1000, new string[] { Permissions.BrowsePermissions }),
            new MenuItem("/backend/roles", localizer["Roles"], 2000, new string[] { Permissions.BrowseRoles }),
            new MenuItem("/backend/users", localizer["Users"], 3000, new string[] { Permissions.BrowseUsers })
          }
        )
      };
    }
  }
}