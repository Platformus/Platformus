// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Core.Backend.Controllers
{
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