// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Infrastructure;

namespace Platformus.ECommerce
{
  public class BackendMetadata : BackendMetadataBase
  {
    public override IEnumerable<BackendMenuGroup> GetBackendMenuGroups(IServiceProvider serviceProvider)
    {
      IStringLocalizer<BackendMetadata> localizer = serviceProvider.GetService<IStringLocalizer<BackendMetadata>>();

      return new BackendMenuGroup[]
      {
        new BackendMenuGroup(
          localizer["Ecommerce"],
          1500,
          new BackendMenuItem[]
          {
            new BackendMenuItem("/backend/catalogs", localizer["Catalogs"], 1000, new string[] { Permissions.BrowseCatalogs }),
            new BackendMenuItem("/backend/categories", localizer["Categories"], 1000, new string[] { Permissions.BrowseCategories }),
            new BackendMenuItem("/backend/products", localizer["Products"], 2000, new string[] { Permissions.BrowseProducts })
          }
        )
      };
    }
  }
}