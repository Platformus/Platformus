// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Core;

namespace Platformus.Website.ResponseCaches;

/// <summary>
/// Describes a HTTP(S) response cache.
/// </summary>
public interface IResponseCache : IParameterized
{
  /// <summary>
  /// Gets a cached HTTP(S) response for the current HTTP(S) request's URL.
  /// If a cached response is missing, the <paramref name="defaultValueFunc"/> function will be executed to get and cache it.
  /// </summary>
  /// <param name="httpContext">Current <see cref="HttpContext"/> to get the request's URL from.</param>
  /// <param name="defaultValueFunc">A function that should be executed to get the HTTP(S) response if it is missing in cache.</param>
  /// <returns></returns>
  public Task<byte[]> GetWithDefaultValueAsync(HttpContext httpContext, Func<Task<byte[]>> defaultValueFunc);

  /// <summary>
  /// Removes all the cached content.
  /// </summary>
  /// <param name="httpContext">Current <see cref="HttpContext"/> to get the needed services from.</param>
  public Task RemoveAllAsync(HttpContext httpContext);
}