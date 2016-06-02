// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Barebone.Backend.Controllers
{
  [Area("Backend")]
  public class DefaultController : ControllerBase
  {
    public DefaultController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index()
    {
      return this.View();
    }
  }
}