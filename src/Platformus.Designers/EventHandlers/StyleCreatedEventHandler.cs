// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Designers.Events;

namespace Platformus.Designers.EventHandlers
{
  public class StyleCreatedEventHandler : IStyleCreatedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(IRequestHandler requestHandler, string filename)
    {
      BandleManager.RebuildAllBundles(requestHandler);
    }
  }
}