// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Platformus.Core.Parameters;
using Platformus.Website.ResponseCaches;

namespace Platformus.Website.Frontend;

public class MemoryResponseCache : IResponseCache
{
  private const string response = "response:";

  public string Description => "Caches responses in memory.";
  public IEnumerable<ParameterGroup> ParameterGroups => new ParameterGroup[] { };

  public async Task<byte[]> GetWithDefaultValueAsync(HttpContext httpContext, Func<Task<byte[]>> defaultValueFunc)
  {
    ICache cache = httpContext.GetCache();
    string key = this.GenerateUniqueKeyForUrl(httpContext.Request.GetEncodedPathAndQuery());
    byte[] responseBody = cache.Get<byte[]>(key);

    if (responseBody != null)
      return responseBody;

    responseBody = await defaultValueFunc();

    cache.Set(key, responseBody);
    return responseBody;
  }

  public async Task RemoveAllAsync(HttpContext httpContext)
  {
    ICache cache = httpContext.GetCache();

    cache.RemoveAll(k => k.StartsWith(response));
  }

  private string GenerateUniqueKeyForUrl(string url)
  {
    return string.Concat(response, url);
  }
}