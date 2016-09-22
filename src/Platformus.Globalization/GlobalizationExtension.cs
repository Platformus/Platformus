// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Infrastructure;

namespace Platformus.Globalization
{
  public class GlobalizationExtension : ExtensionBase
  {
    public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
    {
      get
      {
        return new Dictionary<int, Action<IServiceCollection>>()
        {
          [3000] = services =>
          {
            services.AddLocalization(
              localizationOptions =>
              {
                localizationOptions.ResourcesPath = "Resources";
              }
            );
          }
        };
      }
    }

    public override IEnumerable<KeyValuePair<int, Action<IMvcBuilder>>> AddMvcActionsByPriorities
    {
      get
      {
        return new Dictionary<int, Action<IMvcBuilder>>()
        {
          [3000] = mvcBuilder =>
          {
            mvcBuilder
              .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
              .AddDataAnnotationsLocalization();
          }
        };
      }
    }

    public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
    {
      get
      {
        return new Dictionary<int, Action<IApplicationBuilder>>()
        {
          [3000] = applicationBuilder =>
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

            requestLocalizationOptions.RequestCultureProviders.Insert(0, new RouteValueRequestCultureProvider(this.serviceProvider));
            applicationBuilder.UseRequestLocalization(requestLocalizationOptions);
          }
        };
      }
    }

    public override IBackendMetadata BackendMetadata
    {
      get
      {
        return new BackendMetadata();
      }
    }
  }
}