// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Platformus.Core.Backend.Metadata.Providers
{
  public class DefaultScriptsProvider : IScriptsProvider
  {
    public IEnumerable<Script> GetScripts(HttpContext httpContext)
    {
      List<Script> scripts = new List<Script>();

      foreach (IMetadata metadata in ExtensionManager.GetInstances<IMetadata>())
        foreach (Script script in metadata.GetScripts(httpContext))
          scripts.Add(script);

      return scripts.OrderBy(s => s.Position);
    }
  }
}