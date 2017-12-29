// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Menus.Data.Entities;
using Platformus.Menus.Events;

namespace Platformus.Menus.EventHandlers
{
  public class MenuEditedEventHandler : IMenuEditedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(IRequestHandler requestHandler, Menu menu)
    {
      new SerializationManager(requestHandler).SerializeMenu(menu);
      requestHandler.GetService<ICache>().RemoveMenuViewComponentResult(menu.Code);
    }
  }
}