// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;

namespace Platformus.Barebone.Backend.Metadata.Providers
{
  public class DefaultStyleSheetsProvider : IStyleSheetsProvider
  {
    public IEnumerable<StyleSheet> GetStyleSheets(IRequestHandler requestHandler)
    {
      List<StyleSheet> styleSheets = new List<StyleSheet>();

      foreach (IMetadata metadata in ExtensionManager.GetInstances<IMetadata>())
        foreach (StyleSheet styleSheet in metadata.GetStyleSheets(requestHandler))
          styleSheets.Add(styleSheet);

      return styleSheets.OrderBy(ss => ss.Position);
    }
  }
}