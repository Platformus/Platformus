// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Platformus.Website.ResponseCaches;

namespace Platformus.Website;

/// <summary>
/// Provides ability to invalidate the HTTP(S) response caches and remove all the cached content.
/// </summary>
public class ResponseCacheManager
{
  /// <summary>
  /// Invalidates the HTTP(S) response caches and removes all the cached content.
  /// </summary>
  /// <param name="httpContext">Current <see cref="HttpContext"/> to get the needed services from.</param>
  public static void RemoveAll(HttpContext httpContext)
  {
    foreach (IResponseCache responseCache in ExtensionManager.GetInstances<IResponseCache>())
      responseCache.RemoveAllAsync(httpContext);
  }
}