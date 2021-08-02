// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Extensions;
using Platformus.Website.Filters;
using Platformus.Website.Frontend.Services.Abstractions;

namespace Platformus.Website.Frontend.Services.Defaults
{
  public class DefaultEndpointResolver : IEndpointResolver
  {
    public async Task<Data.Entities.Endpoint> ResolveAsync(HttpContext httpContext)
    {
      if (httpContext.Request.Path.StartsWithSegments(new PathString("/backend")))
        return null;

      IEnumerable<Data.Entities.Endpoint> endpoints = await this.GetEndpointsAsync(httpContext);

      foreach (Data.Entities.Endpoint endpoint in endpoints)
        if (this.IsMatch(endpoint.UrlTemplate, httpContext.Request.GetUrl().Substring(1)))
          return endpoint;

      return null;
    }

    public async Task<IEnumerable<Data.Entities.Endpoint>> GetEndpointsAsync(HttpContext httpContext)
    {
      ICache cache = httpContext.GetCache();

      return await cache.GetWithDefaultValueAsync(
        "endpoints",
        async () => await httpContext.GetStorage().GetRepository<int, Data.Entities.Endpoint, EndpointFilter>().GetAllAsync(
          sorting: "+position",
          inclusions: new Inclusion<Data.Entities.Endpoint>[] {
            new Inclusion<Data.Entities.Endpoint>("EndpointPermissions.Permission"),
            new Inclusion<Data.Entities.Endpoint>(e => e.DataSources)
          }
        ),
        new CacheEntryOptions(priority: CacheEntryPriority.NeverRemove)
      );
    }

    private bool IsMatch(string urlTemplate, string url)
    {
      if (urlTemplate == "{*url}")
        return true;

      if (string.IsNullOrEmpty(urlTemplate) && string.IsNullOrEmpty(url))
        return true;

      if (string.Equals(urlTemplate, url, StringComparison.OrdinalIgnoreCase))
        return true;

      if (string.IsNullOrEmpty(urlTemplate))
        return false;

      return urlTemplate.Count(ch => ch == '/') == url.Count(ch => ch == '/') && Regex.IsMatch(url, this.GetRegexFromUrlTemplate(urlTemplate));
    }

    private string GetRegexFromUrlTemplate(string urlTemplate)
    {
      return Regex.Replace(urlTemplate, "{.+?}", "(.+)");
    }
  }
}