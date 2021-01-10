// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Platformus.Website.ResponseCaches;

namespace Platformus.Website
{
  public class ResponseCacheManager
  {
    public static void RemoveAll(HttpContext httpContext)
    {
      foreach (IResponseCache responseCache in ExtensionManager.GetInstances<IResponseCache>())
        responseCache.RemoveAllAsync(httpContext);
    }
  }
}