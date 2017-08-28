// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Infrastructure;

namespace Platformus.Menus
{
  public class BackendMetadata : BackendMetadataBase
  {
    public override IEnumerable<BackendMenuGroup> GetBackendMenuGroups(IServiceProvider serviceProvider)
    {
      IStringLocalizer<BackendMetadata> localizer = serviceProvider.GetService<IStringLocalizer<BackendMetadata>>();

      return new BackendMenuGroup[]
      {
          new BackendMenuGroup(
          localizer["Content"],
          1000,
          new BackendMenuItem[]
          {
            new BackendMenuItem("/backend/menus", localizer["Menus"], 2000, new string[] { Permissions.BrowseMenus })
          }
        )
      };
    }
  }
}