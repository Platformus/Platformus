// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone;
using Platformus.Globalization.Services.Abstractions;

namespace Platformus.Globalization.Frontend
{
  public class RouteValueRequestCultureProvider : RequestCultureProvider
  {
    private IServiceProvider serviceProvider;

    public RouteValueRequestCultureProvider(IServiceProvider serviceProvider)
    {
      this.serviceProvider = serviceProvider;
    }

    public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    {
      string cultureCode = null;

      // TODO: get rid of these hardcoded strings
      if (httpContext.Request.Path.HasValue && (httpContext.Request.Path.Value == "/" || httpContext.Request.Path.Value.Contains("/img") || httpContext.Request.Path.Value.Contains("/api/v1/")))
        cultureCode = this.GetDefaultCultureCode();

      else if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value.Length >= 4 && httpContext.Request.Path.Value[0] == '/' && httpContext.Request.Path.Value[3] == '/')
      {
        cultureCode = httpContext.Request.Path.Value.Substring(1, 2);

        if (!this.CheckCulture(cultureCode))
          throw new HttpException(HttpStatusCode.NotFound);
      }

      else throw new HttpException(HttpStatusCode.NotFound);

      ProviderCultureResult requestCulture = new ProviderCultureResult(cultureCode);

      return Task.FromResult(requestCulture);
    }

    private string GetDefaultCultureCode()
    {
      Data.Entities.Culture frontendDefaultCulture = this.serviceProvider.GetService<ICultureManager>().GetFrontendDefaultCulture();

      if (frontendDefaultCulture == null)
        return DefaultCulture.Code;

      return frontendDefaultCulture.Code;
    }

    private bool CheckCulture(string cultureCode)
    {
      ICultureManager cultureManager = this.serviceProvider.GetService<ICultureManager>();

      if (cultureManager.GetNotNeutralCultures().Count() == 0)
        return cultureCode == DefaultCulture.Code;

      return cultureManager.GetNotNeutralCultures().Any(c => c.Code == cultureCode);
    }
  }
}