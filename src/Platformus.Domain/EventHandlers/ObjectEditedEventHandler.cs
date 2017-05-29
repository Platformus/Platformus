// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain
{
  public class ObjectEditedEventHandler : IObjectEditedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(IRequestHandler requestHandler, Object oldObject, Object newObject)
    {
      new SerializationManager(requestHandler).SerializeObject(newObject);

      {
        string urlPropertyStringValue = new ObjectManager(requestHandler).GetUrlPropertyStringValue(oldObject);

        requestHandler.HttpContext.RequestServices.GetService<ICache>().RemovePageActionResult(urlPropertyStringValue);
      }

      {
        string urlPropertyStringValue = new ObjectManager(requestHandler).GetUrlPropertyStringValue(newObject);

        requestHandler.HttpContext.RequestServices.GetService<ICache>().RemovePageActionResult(urlPropertyStringValue);
      }
    }
  }
}