// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone;

namespace Platformus.Globalization
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

      if (httpContext.Request.Path.HasValue && (httpContext.Request.Path.Value == "/" || httpContext.Request.Path.Value.Contains("/backend/") || httpContext.Request.Path.Value.Contains("/api/v1/")))
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
      IStorage storage = this.serviceProvider.GetService<IStorage>();
      Data.Entities.Culture defaultCulture = CultureManager.GetDefaultCulture(storage);

      if (defaultCulture == null)
        return DefaultCulture.Code;

      return defaultCulture.Code;
    }

    private bool CheckCulture(string cultureCode)
    {
      IStorage storage = this.serviceProvider.GetService<IStorage>();

      if (CultureManager.GetNotNeutralCultures(storage).Count() == 0)
        return cultureCode == DefaultCulture.Code;

      return CultureManager.GetNotNeutralCultures(storage).Any(c => c.Code == cultureCode);
    }
  }
}