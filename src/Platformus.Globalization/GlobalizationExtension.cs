// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Localization;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Infrastructure;

namespace Platformus.Globalization
{
  public class GlobalizationExtension : IExtension
  {
    private IConfigurationRoot configurationRoot;

    public string Name
    {
      get
      {
        return "Globalization Extension";
      }
    }

    public IEnumerable<BackendMenuGroup> BackendMenuGroups
    {
      get
      {
        return new BackendMenuGroup[]
        {
          new BackendMenuGroup(
            "Settings",
            2000,
            new BackendMenuItem[]
            {
              new BackendMenuItem("/backend/cultures", "Cultures", 1000)
            }
          )
        };
      }
    }

    public void SetConfigurationRoot(IConfigurationRoot configurationRoot)
    {
      this.configurationRoot = configurationRoot;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddLocalization();
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
      RequestLocalizationOptions requestLocalizationOptions = new RequestLocalizationOptions();

      requestLocalizationOptions.SupportedCultures = new List<CultureInfo>
      {
        new CultureInfo("en"),
        new CultureInfo("uk")
      };

      requestLocalizationOptions.SupportedUICultures = new List<CultureInfo>
      {
        new CultureInfo("en"),
        new CultureInfo("uk")
      };

      requestLocalizationOptions.RequestCultureProviders.Insert(0, new RouteValueRequestCultureProvider());
      applicationBuilder.UseRequestLocalization(requestLocalizationOptions, new RequestCulture("en"));
    }

    public void RegisterRoutes(IRouteBuilder routeBuilder)
    {
    }
  }
}