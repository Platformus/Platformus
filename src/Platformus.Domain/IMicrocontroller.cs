// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain
{
  public interface IMicrocontroller
  {
    IActionResult Invoke(IRequestHandler requestHandler, Microcontroller microcontroller, IEnumerable<KeyValuePair<string, string>> parameters);
  }
}