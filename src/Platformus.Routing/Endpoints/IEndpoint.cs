// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Endpoints
{
  public interface IEndpoint
  {
    IEnumerable<EndpointParameterGroup> ParameterGroups { get; }
    string Description { get; }

    IActionResult Invoke(IRequestHandler requestHandler, Endpoint endpoint, IEnumerable<KeyValuePair<string, string>> arguments);
  }
}