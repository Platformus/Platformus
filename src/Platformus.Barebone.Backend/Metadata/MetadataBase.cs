// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Barebone.Backend.Metadata
{
  public abstract class MetadataBase : IMetadata
  {
    public virtual IEnumerable<StyleSheet> GetStyleSheets(IRequestHandler requestHandler) => new StyleSheet[] { };
    public virtual IEnumerable<Script> GetScripts(IRequestHandler requestHandler) => new Script[] { };
    public virtual IEnumerable<MenuGroup> GetMenuGroups(IRequestHandler requestHandler) => new MenuGroup[] { };
  }
}