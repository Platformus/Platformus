// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone;
using Platformus.Globalization;
using Platformus.Globalization.Data.Models;
using Platformus.Menus.Data.Models;

namespace Platformus.Menus
{
  public class MenuEditedEventHandler : IMenuEditedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(IRequestHandler requestHandler, Menu oldMenu, Menu newMenu)
    {
      new SerializationManager(requestHandler).SerializeMenu(newMenu);

      if (oldMenu != null)
        foreach (Culture culture in CultureManager.GetNotNeutralCultures(requestHandler.Storage))
          requestHandler.HttpContext.RequestServices.GetService<ICache>().RemoveMenuViewComponentResult(oldMenu.Code, culture.Code);

      foreach (Culture culture in CultureManager.GetNotNeutralCultures(requestHandler.Storage))
        requestHandler.HttpContext.RequestServices.GetService<ICache>().RemoveMenuViewComponentResult(newMenu.Code, culture.Code);
    }
  }
}