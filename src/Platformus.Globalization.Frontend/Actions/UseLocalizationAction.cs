// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Linq;
using ExtCore.Data.Abstractions;
using ExtCore.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Configurations;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Frontend.Actions
{
  public class UseLocalizationAction : IConfigureAction
  {
    public int Priority => 4000;

    public void Execute(IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
    {
      RequestLocalizationOptions requestLocalizationOptions = new RequestLocalizationOptions();
      IStorage storage = serviceProvider.GetService<IStorage>();

      if (storage == null)
      {
        requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(DefaultCulture.Code);
        requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
          new CultureInfo[] { new CultureInfo(DefaultCulture.Code) }.ToList();
      }

      else
      {
        Culture frontendDefaultCulture = serviceProvider.GetService<ICultureManager>().GetFrontendDefaultCulture();

        if (frontendDefaultCulture == null)
          requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(DefaultCulture.Code);

        else requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(frontendDefaultCulture.Code);

        if (serviceProvider.GetService<ICultureManager>().GetNotNeutralCultures().Count() == 0)
        {
          requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
            new CultureInfo[] { new CultureInfo(DefaultCulture.Code) }.ToList();
        }

        else
        {
          requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
            serviceProvider.GetService<ICultureManager>().GetNotNeutralCultures().Select(c => new CultureInfo(c.Code)).ToList();
        }

        if (this.MustSpecifyCultureInUrl(serviceProvider))
          requestLocalizationOptions.RequestCultureProviders.Insert(0, new RouteValueRequestCultureProvider(serviceProvider));
      }

      applicationBuilder.UseWhen(
        context => !context.Request.Path.StartsWithSegments(new PathString("/backend")),
        frontendApplicationBuilder =>
        {
          frontendApplicationBuilder.UseRequestLocalization(requestLocalizationOptions);
        }
      );
    }

    private bool MustSpecifyCultureInUrl(IServiceProvider serviceProvider)
    {
      return serviceProvider.GetService<IConfigurationManager>()["Globalization", "SpecifyCultureInUrl"] != "no";
    }
  }
}