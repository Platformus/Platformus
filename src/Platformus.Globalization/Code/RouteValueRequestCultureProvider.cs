// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Platformus.Globalization
{
  public class RouteValueRequestCultureProvider : RequestCultureProvider
  {
    public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    {
      string cultureCode = null;

      if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value.Length >= 4 && httpContext.Request.Path.Value[0] == '/' && httpContext.Request.Path.Value[3] == '/')
        cultureCode = httpContext.Request.Path.Value.Substring(1, 2);

      else cultureCode = "en";

      ProviderCultureResult requestCulture = new ProviderCultureResult(cultureCode);

      return Task.FromResult(requestCulture);
    }
  }
}