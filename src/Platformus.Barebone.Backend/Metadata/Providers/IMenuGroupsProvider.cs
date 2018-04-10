// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Barebone.Backend.Metadata.Providers
{
  public interface IMenuGroupsProvider
  {
    IEnumerable<MenuGroup> GetMenuGroups(IRequestHandler requestHandler);
  }
}