// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Linq;
using ExtCore.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Core.Data.Entities;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Frontend.Actions
{
  public class UseRequestLocalizationAction : IConfigureAction
  {
    public int Priority => 1000;

    public void Execute(IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
    {
      serviceProvider = serviceProvider.CreateScope().ServiceProvider;

      RequestLocalizationOptions requestLocalizationOptions = new RequestLocalizationOptions();
      ICultureManager cultureManager = serviceProvider.GetService<ICultureManager>();
      Culture frontendDefaultCulture = cultureManager.GetFrontendDefaultCultureAsync().Result;

      if (frontendDefaultCulture == null)
        requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(DefaultCulture.Id);

      else requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(frontendDefaultCulture.Id);

      if (cultureManager.GetNotNeutralCulturesAsync().Result.Count() == 0)
      {
        requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
          new CultureInfo[] { new CultureInfo(DefaultCulture.Id) }.ToList();
      }

      else
      {
        requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
          cultureManager.GetNotNeutralCulturesAsync().Result.Select(c => new CultureInfo(c.Id)).ToList();
      }

      requestLocalizationOptions.RequestCultureProviders.Clear();

      if (serviceProvider.GetService<IConfigurationManager>()["Globalization", "SpecifyCultureInUrl"] == "yes")
        requestLocalizationOptions.RequestCultureProviders.Insert(0, new RouteValueRequestCultureProvider());

      applicationBuilder.UseWhen(
        context => !context.Request.Path.StartsWithSegments(new PathString("/backend")),
        frontendApplicationBuilder =>
        {
          frontendApplicationBuilder.UseRequestLocalization(requestLocalizationOptions);
        }
      );
    }
  }
}