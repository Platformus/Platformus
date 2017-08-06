// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Routing.Backend.Controllers
{
  [Area("Backend")]
  public class RoutingController : Platformus.Barebone.Backend.Controllers.ControllerBase
  {
    public RoutingController(IStorage storage)
      : base(storage)
    {
    }
  }
}