// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
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

    public IFrontendMetadata FrontendMetadata
    {
      get
      {
        return null;
      }
    }

    public IBackendMetadata BackendMetadata
    {
      get
      {
        return new BackendMetadata();
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

      string defaultCulture = this.configurationRoot["Globalization:DefaultCulture"];

      if (string.IsNullOrEmpty(defaultCulture))
        defaultCulture = "en";

      requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(defaultCulture);

      string supportedCultures = this.configurationRoot["Globalization:SupportedCultures"];

      if (!string.IsNullOrEmpty(supportedCultures))
      {
        requestLocalizationOptions.SupportedCultures = new List<CultureInfo>(supportedCultures.Split(',').Select(c => new CultureInfo(c)));
        requestLocalizationOptions.SupportedUICultures = new List<CultureInfo>(supportedCultures.Split(',').Select(c => new CultureInfo(c)));
      }

      requestLocalizationOptions.RequestCultureProviders.Insert(0, new RouteValueRequestCultureProvider());
      applicationBuilder.UseRequestLocalization(requestLocalizationOptions);
    }

    public void RegisterRoutes(IRouteBuilder routeBuilder)
    {
    }
  }
}