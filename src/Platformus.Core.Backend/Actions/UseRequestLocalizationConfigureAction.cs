// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
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
using Platformus.Core.Data.Entities;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Backend.Actions
{
  public class UseRequestLocalizationConfigureAction : IConfigureAction
  {
    public int Priority => 1000;

    public void Execute(IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
    {
      serviceProvider = serviceProvider.CreateScope().ServiceProvider;

      RequestLocalizationOptions requestLocalizationOptions = new RequestLocalizationOptions();
      IStorage storage = serviceProvider.GetService<IStorage>();

      if (storage == null)
      {
        requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(DefaultCulture.Id);
        requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
          new CultureInfo[] { new CultureInfo(DefaultCulture.Id) }.ToList();
      }

      else
      {
        Culture backendDefaultCulture = serviceProvider.GetService<ICultureManager>().GetBackendDefaultCultureAsync().Result;

        if (backendDefaultCulture == null)
        {
          requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(DefaultCulture.Id);
          requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
            new CultureInfo[] { new CultureInfo(DefaultCulture.Id) }.ToList();
        }

        else
        {
          requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(backendDefaultCulture.Id);
          requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
            new CultureInfo[] { new CultureInfo(backendDefaultCulture.Id) }.ToList();
        }
      }

      applicationBuilder.UseWhen(
        context => context.Request.Path.StartsWithSegments(new PathString("/backend")),
        backendApplicationBuilder => backendApplicationBuilder.UseRequestLocalization(requestLocalizationOptions)
      );
    }
  }
}