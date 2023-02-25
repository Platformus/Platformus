// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core;

namespace Platformus.Website.RequestProcessors;

/// <summary>
/// Describes a request processor. Endpoints use request processors selected by the users to process requests.
/// Request processor takes any information from the HTTP(S) request (URL, cookies etc.) and returns action result.
/// </summary>
public interface IRequestProcessor : IParameterized
{
  /// <summary>
  /// Processes the HTTP(S) request and returns the response.
  /// </summary>
  /// <param name="httpContext">Current <see cref="HttpContext"/> to get the request from.</param>
  /// <param name="endpoint">An endpoint that uses the request processor.</param>
  Task<IActionResult> ProcessAsync(HttpContext httpContext, Data.Entities.Endpoint endpoint);
}