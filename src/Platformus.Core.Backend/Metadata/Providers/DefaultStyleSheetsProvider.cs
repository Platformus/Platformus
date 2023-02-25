// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Platformus.Core.Backend.Metadata.Providers;

public class DefaultStyleSheetsProvider : IStyleSheetsProvider
{
  public IEnumerable<StyleSheet> GetStyleSheets(HttpContext httpContext)
  {
    return ExtensionManager.GetInstances<IMetadata>()
      .SelectMany(m => m.GetStyleSheets(httpContext))
      .OrderBy(ss => ss.Position);
  }
}