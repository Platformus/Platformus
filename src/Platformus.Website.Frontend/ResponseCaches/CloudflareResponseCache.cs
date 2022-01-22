// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Platformus.Core.Parameters;
using Platformus.Website.ResponseCaches;

namespace Platformus.Website.Frontend
{
  public class CloudflareResponseCache : IResponseCache
  {
    public string Description => "Caches responses on the Cloudflare side.";
    public IEnumerable<ParameterGroup> ParameterGroups => new ParameterGroup[] { };

    public async Task<byte[]> GetWithDefaultValueAsync(HttpContext httpContext, Func<Task<byte[]>> defaultValueFunc)
    {
      return await defaultValueFunc();
    }

    public async Task RemoveAllAsync(HttpContext httpContext)
    {
      CloudflareOptions options = httpContext.RequestServices.GetService<IOptions<CloudflareOptions>>()?.Value;

      if (options == null)
        return;

      using (HttpClient httpClient = new HttpClient())
      {
        httpClient.DefaultRequestHeaders.Add("X-Auth-Email", options.Email);
        httpClient.DefaultRequestHeaders.Add("X-Auth-Key", options.Key);

        string purgeEverything = JsonConvert.SerializeObject(new { purge_everything = true });

        using (HttpContent httpContent = new StringContent(purgeEverything, Encoding.UTF8, "application/json"))
          await httpClient.PostAsync($"https://api.cloudflare.com/client/v4/zones/{options.ZoneKey}/purge_cache", httpContent);
      }
    }
  }
}