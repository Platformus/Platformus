// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Barebone.Backend.Metadata
{
  public interface IMetadata
  {
    IEnumerable<StyleSheet> GetStyleSheets(IRequestHandler requestHandler);
    IEnumerable<Script> GetScripts(IRequestHandler requestHandler);
    IEnumerable<MenuGroup> GetMenuGroups(IRequestHandler requestHandler);
  }
}