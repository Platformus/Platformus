// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Platformus.Core.Extensions;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Frontend
{
  public class RouteValueRequestCultureProvider : RequestCultureProvider
  {
    public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    {
      string cultureId = await this.GetDefaultCultureCodeAsync(httpContext);
      string url = httpContext.Request.Path;

      if (url.Length >= 4 && url[0] == '/' && url[3] == '/')
      {
        cultureId = httpContext.Request.Path.Value.Substring(1, 2);

        if (!await this.CheckCultureAsync(httpContext, cultureId))
          throw new HttpException(HttpStatusCode.NotFound);
      }

      ProviderCultureResult requestCulture = new ProviderCultureResult(cultureId);

      return requestCulture;
    }

    private async Task<string> GetDefaultCultureCodeAsync(HttpContext httpContext)
    {
      Data.Entities.Culture frontendDefaultCulture = await httpContext.GetCultureManager().GetFrontendDefaultCultureAsync();

      if (frontendDefaultCulture == null)
        return DefaultCulture.Id;

      return frontendDefaultCulture.Id;
    }

    private async Task<bool> CheckCultureAsync(HttpContext httpContext, string cultureId)
    {
      ICultureManager cultureManager = httpContext.GetCultureManager();

      if ((await cultureManager.GetNotNeutralCulturesAsync()).Count() == 0)
        return cultureId == DefaultCulture.Id;

      return (await cultureManager.GetNotNeutralCulturesAsync()).Any(c => c.Id == cultureId);
    }
  }
}