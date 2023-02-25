// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Platformus.Core.Backend.Metadata;

public abstract class MetadataBase : IMetadata
{
  public virtual IEnumerable<StyleSheet> GetStyleSheets(HttpContext httpContext) => new StyleSheet[] { };
  public virtual IEnumerable<Script> GetScripts(HttpContext httpContext) => new Script[] { };
  public virtual IEnumerable<MenuGroup> GetMenuGroups(HttpContext httpContext) => new MenuGroup[] { };
  public virtual IEnumerable<DashboardWidget> GetDashboardWidgets(HttpContext httpContext) => new DashboardWidget[] { };
}