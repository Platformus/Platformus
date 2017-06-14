// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone;
using Platformus.Domain.Data.Models;
using Platformus.Globalization;
using Platformus.Globalization.Data.Models;

namespace Platformus.Domain
{
  public class ObjectEditedEventHandler : IObjectEditedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(IRequestHandler requestHandler, Object oldObject, Object newObject)
    {
      new SerializationManager(requestHandler).SerializeObject(newObject);

      if (oldObject != null)
      {
        string urlPropertyStringValue = new ObjectManager(requestHandler).GetUrlPropertyStringValue(oldObject);

        foreach (Culture culture in CultureManager.GetNotNeutralCultures(requestHandler.Storage))
          requestHandler.HttpContext.RequestServices.GetService<ICache>().RemovePageActionResult(urlPropertyStringValue, culture.Code);
      }

      {
        string urlPropertyStringValue = new ObjectManager(requestHandler).GetUrlPropertyStringValue(newObject);

        foreach (Culture culture in CultureManager.GetNotNeutralCultures(requestHandler.Storage))
          requestHandler.HttpContext.RequestServices.GetService<ICache>().RemovePageActionResult(urlPropertyStringValue, culture.Code);
      }
    }
  }
}