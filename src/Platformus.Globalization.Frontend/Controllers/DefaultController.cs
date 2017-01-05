// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Globalization.Frontend.Controllers
{
  [AllowAnonymous]
  public class DefaultController : Barebone.Frontend.Controllers.ControllerBase
  {
    public DefaultController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string url)
    {
      foreach (IDefaultRouteHandler defaultRouteHandler in ExtensionManager.GetInstances<IDefaultRouteHandler>())
      {
        ActionResult result = defaultRouteHandler.TryHandle(this, url);

        if (result != null)
          return result;
      }

      return this.NotFound();
    }
  }
}