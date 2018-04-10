// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using ExtCore.Data.Abstractions;
using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Routing.Frontend.Controllers
{
  public class DefaultController : Barebone.Frontend.Controllers.ControllerBase
  {
    public DefaultController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string url)
    {
      foreach (IMiddleware middleware in ExtensionManager.GetInstances<IMiddleware>().OrderBy(m => m.Priority))
      {
        IActionResult result = middleware.Invoke(this, url);

        if (result != null)
          return result;
      }

      return this.NotFound();
    }
  }
}