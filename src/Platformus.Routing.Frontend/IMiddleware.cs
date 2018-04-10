// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;

namespace Platformus.Routing.Frontend
{
  public interface IMiddleware
  {
    int Priority { get; }
    IActionResult Invoke(IRequestHandler handler, string url);
  }
}