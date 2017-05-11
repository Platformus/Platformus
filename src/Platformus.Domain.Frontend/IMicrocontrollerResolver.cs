// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Frontend
{
  public interface IMicrocontrollerResolver
  {
    Microcontroller GetMicrocontroller(IRequestHandler requestHandler, string url);
    IEnumerable<KeyValuePair<string, string>> GetParameters(string urlTemplate, string url);
  }
}
