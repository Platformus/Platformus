// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Parameters;

namespace Platformus.Website.Endpoints
{
  public interface IEndpoint
  {
    IEnumerable<ParameterGroup> ParameterGroups { get; }
    string Description { get; }

    Task<IActionResult> InvokeAsync(HttpContext httpContext, Data.Entities.Endpoint endpoint);
  }
}