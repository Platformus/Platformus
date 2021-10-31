// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Parameters;

namespace Platformus.Website.RequestProcessors
{
  /// <summary>
  /// Describes a request processor. Endpoints use request processors selected by the users to process requests.
  /// Request processor takes any information from the HTTP(S) request (URL, cookies etc.) and returns action result.
  /// </summary>
  public interface IRequestProcessor
  {
    /// <summary>
    /// Gets the parameter groups with the parameters the request processor needs from the users.
    /// </summary>
    IEnumerable<ParameterGroup> ParameterGroups { get; }

    /// <summary>
    /// Gets description that is shown to a user to describe the request processor.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Processes the HTTP(S) request and returns the response.
    /// </summary>
    /// <param name="httpContext">Current <see cref="HttpContext"/> to get the request from.</param>
    /// <param name="endpoint">An endpoint that uses the request processor.</param>
    Task<IActionResult> ProcessAsync(HttpContext httpContext, Data.Entities.Endpoint endpoint);
  }
}