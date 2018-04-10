// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.Metadata;

namespace Platformus.Configurations.Backend.Metadata
{
  public class Metadata : MetadataBase
  {
    public override IEnumerable<MenuGroup> GetMenuGroups(IRequestHandler requestHandler)
    {
      IStringLocalizer<Metadata> localizer = requestHandler.GetService<IStringLocalizer<Metadata>>();

      return new MenuGroup[]
      {
        new MenuGroup(
          localizer["Administration"],
          3000,
          new MenuItem[]
          {
            new MenuItem("/backend/configurations", localizer["Configurations"], 4000, new string[] { Permissions.BrowseConfigurations })
          }
        )
      };
    }
  }
}