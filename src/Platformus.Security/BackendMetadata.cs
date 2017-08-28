// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Infrastructure;

namespace Platformus.Security
{
  public class BackendMetadata : BackendMetadataBase
  {
    public override IEnumerable<BackendMenuGroup> GetBackendMenuGroups(IServiceProvider serviceProvider)
    {
      IStringLocalizer<BackendMetadata> localizer = serviceProvider.GetService<IStringLocalizer<BackendMetadata>>();

      return new BackendMenuGroup[]
      {
        new BackendMenuGroup(
          localizer["Audience"],
          2000,
          new BackendMenuItem[]
          {
            new BackendMenuItem("/backend/permissions", localizer["Permissions"], 1000, new string[] { Permissions.BrowsePermissions }),
            new BackendMenuItem("/backend/roles", localizer["Roles"], 2000, new string[] { Permissions.BrowseRoles }),
            new BackendMenuItem("/backend/users", localizer["Users"], 3000, new string[] { Permissions.BrowseUsers })
          }
        )
      };
    }
  }
}