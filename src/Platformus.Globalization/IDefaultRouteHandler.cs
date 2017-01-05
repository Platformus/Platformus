// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;

namespace Platformus.Globalization
{
  public interface IDefaultRouteHandler
  {
    ActionResult TryHandle(IRequestHandler handler, string url);
  }
}