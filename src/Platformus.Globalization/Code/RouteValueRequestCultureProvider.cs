// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Globalization.Data.Models;

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

      if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value.Length >= 4 && httpContext.Request.Path.Value[0] == '/' && httpContext.Request.Path.Value[3] == '/')
        cultureCode = httpContext.Request.Path.Value.Substring(1, 2);

      else
      {
        IStorage storage = this.serviceProvider.GetService<IStorage>();

        if (storage == null)
          cultureCode = "en";

        else
        {
          Culture defaultCulture = CultureManager.GetDefaultCulture(storage);

          if (defaultCulture == null)
            cultureCode = "en";

          else cultureCode = defaultCulture.Code;
        }
      }

      ProviderCultureResult requestCulture = new ProviderCultureResult(cultureCode);

      return Task.FromResult(requestCulture);
    }
  }
}