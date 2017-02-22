// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Configurations;
using Platformus.Globalization.Data.Models;
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
            IStorage storage = this.serviceProvider.GetService<IStorage>();

            if (storage == null)
            {
              requestLocalizationOptions.DefaultRequestCulture = new RequestCulture("en");
              requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
                new CultureInfo[] { new CultureInfo("en") }.ToList();
            }

            else
            {
              Culture defaultCulture = CultureManager.GetDefaultCulture(storage);

              if (defaultCulture == null)
                requestLocalizationOptions.DefaultRequestCulture = new RequestCulture("en");

              else requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(defaultCulture.Code);

              if (CultureManager.GetCultures(storage).Count() == 0)
              {
                requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
                  new CultureInfo[] { new CultureInfo("en") }.ToList();
              }

              else
              {
                requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
                  CultureManager.GetNotNeutralCultures(storage).Select(c => new CultureInfo(c.Code)).ToList();
              }
            }

            requestLocalizationOptions.RequestCultureProviders.Insert(0, new RouteValueRequestCultureProvider(this.serviceProvider));
            applicationBuilder.UseRequestLocalization(requestLocalizationOptions);
          }
        };
      }
    }

    public override IEnumerable<KeyValuePair<int, Action<IRouteBuilder>>> UseMvcActionsByPriorities
    {
      get
      {
        return new Dictionary<int, Action<IRouteBuilder>>()
        {
          [10000] = routeBuilder =>
          {
            string defaultCultureCode = "en";
            bool specifyCultureInUrl = true;
            IStorage storage = this.serviceProvider.GetService<IStorage>();

            if (storage == null)
            {
              routeBuilder.MapRoute(name: "Default", template: "", defaults: new { controller = "Installation", action = "Index" });
              return;
            }

            Culture defaultCulture = CultureManager.GetDefaultCulture(storage);

            if (defaultCulture != null)
              defaultCultureCode = defaultCulture.Code;

            specifyCultureInUrl = new ConfigurationManager(storage)["Globalization", "SpecifyCultureInUrl"] != "no";

            string template = string.Empty;

            if (specifyCultureInUrl)
              template = "{culture=" + defaultCultureCode + "}/{*url}";

            else template = "{*url}";

            routeBuilder.MapRoute(name: "Default", template: template, defaults: new { controller = "Default", action = "Index" });
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