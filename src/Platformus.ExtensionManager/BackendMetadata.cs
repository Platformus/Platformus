// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Infrastructure;

namespace Platformus.ExtensionManager
{
  public class BackendMetadata : BackendMetadataBase
  {
    public override IEnumerable<BackendMenuGroup> GetBackendMenuGroups(IServiceProvider serviceProvider)
    {
      IStringLocalizer<BackendMetadata> localizer = serviceProvider.GetService<IStringLocalizer<BackendMetadata>>();

      return new BackendMenuGroup[]
      {
        new BackendMenuGroup(
          localizer["Development"],
          4000,
          new BackendMenuItem[]
          {
            new BackendMenuItem("/backend/extensionmanager", localizer["Extension manager"], 1000, new string[] { Permissions.BrowseExtensionManager })
          }
        )
      };
    }
  }
}