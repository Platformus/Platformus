// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;

namespace Platformus.Barebone.Backend.Metadata.Providers
{
  public class DefaultScriptsProvider : IScriptsProvider
  {
    public IEnumerable<Script> GetScripts(IRequestHandler requestHandler)
    {
      List<Script> scripts = new List<Script>();

      foreach (IMetadata metadata in ExtensionManager.GetInstances<IMetadata>())
        foreach (Script script in metadata.GetScripts(requestHandler))
          scripts.Add(script);

      return scripts.OrderBy(s => s.Position);
    }
  }
}