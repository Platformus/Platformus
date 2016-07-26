// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Infrastructure;

namespace Platformus.Security
{
  public class BackendMetadata : BackendMetadataBase
  {
    public override IEnumerable<BackendMenuGroup> BackendMenuGroups
    {
      get
      {
        return new BackendMenuGroup[]
        {
          new BackendMenuGroup(
            "Audience",
            3000,
            new BackendMenuItem[]
            {
              new BackendMenuItem("/backend/permissions", "Permissions", 1000),
              new BackendMenuItem("/backend/roles", "Roles", 2000),
              new BackendMenuItem("/backend/users", "Users", 3000)
            }
          )
        };
      }
    }
  }
}