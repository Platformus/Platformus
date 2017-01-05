// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Domain.Frontend.ViewModels.Shared;
using Platformus.Globalization;

namespace Platformus.Domain.Frontend
{
  public class DefaultRouteHandler : IDefaultRouteHandler
  {
    public ActionResult TryHandle(IRequestHandler handler, string url)
    {
      url = string.Format("/{0}", url);

      CachedObject cachedObject = handler.Storage.GetRepository<ICachedObjectRepository>().WithCultureIdAndUrl(
        CultureManager.GetCurrentCulture(handler.Storage).Id, url
      );

      if (cachedObject != null)
      {
        ObjectViewModel result = new ObjectViewModelFactory(handler).Create(cachedObject);

        return (handler as Platformus.Barebone.Frontend.Controllers.ControllerBase).View(result.ViewName, result);
      }

      Object @object = handler.Storage.GetRepository<IObjectRepository>().WithUrl(url);

      if (@object != null)
      {
        ObjectViewModel result = new ObjectViewModelFactory(handler).Create(@object);

        return (handler as Platformus.Barebone.Frontend.Controllers.ControllerBase).View(result.ViewName, result);
      }

      return null;
    }
  }
}