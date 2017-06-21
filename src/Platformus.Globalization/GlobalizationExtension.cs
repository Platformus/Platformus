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
              requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(DefaultCulture.Code);
              requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
                new CultureInfo[] { new CultureInfo(DefaultCulture.Code) }.ToList();
            }

            else
            {
              Culture defaultCulture = CultureManager.GetDefaultCulture(storage);

              if (defaultCulture == null)
                requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(DefaultCulture.Code);

              else requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(defaultCulture.Code);

              if (CultureManager.GetNotNeutralCultures(storage).Count() == 0)
              {
                requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
                  new CultureInfo[] { new CultureInfo(DefaultCulture.Code) }.ToList();
              }

              else
              {
                requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
                  CultureManager.GetNotNeutralCultures(storage).Select(c => new CultureInfo(c.Code)).ToList();
              }

              if (this.MustSpecifyCultureInUrl(storage))
                requestLocalizationOptions.RequestCultureProviders.Insert(0, new RouteValueRequestCultureProvider(this.serviceProvider));
            }

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
            IStorage storage = this.serviceProvider.GetService<IStorage>();

            if (storage == null)
            {
              routeBuilder.MapRoute(name: "Default", template: "", defaults: new { controller = "Installation", action = "Index" });
              return;
            }

            string template = string.Empty;

            if (this.MustSpecifyCultureInUrl(storage))
              template = "{culture=" + this.GetDefaultCultureCode(storage) + "}/{*url}";

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

    private bool MustSpecifyCultureInUrl(IStorage storage)
    {
      return new ConfigurationManager(storage)["Globalization", "SpecifyCultureInUrl"] != "no";
    }

    private string GetDefaultCultureCode(IStorage storage)
    {
      Culture defaultCulture = CultureManager.GetDefaultCulture(storage);

      if (defaultCulture == null)
        return DefaultCulture.Code;

      return defaultCulture.Code;
    }
  }
}