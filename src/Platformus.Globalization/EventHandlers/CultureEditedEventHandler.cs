// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Data.Entities;
using Platformus.Globalization.Events;
using Platformus.Globalization.Services.Abstractions;

namespace Platformus.Globalization.EventHandlers
{
  public class CultureEditedEventHandler : ICultureEditedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(IRequestHandler requestHandler, Culture culture)
    {
      requestHandler.GetService<ICultureManager>().InvalidateCache();
    }
  }
}