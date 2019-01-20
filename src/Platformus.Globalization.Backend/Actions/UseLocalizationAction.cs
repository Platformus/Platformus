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
using Platformus.Globalization.Data.Entities;
using Platformus.Globalization.Services.Abstractions;

namespace Platformus.Globalization.Backend.Actions
{
  public class UseLocalizationAction : IConfigureAction
  {
    public int Priority => 4010;

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
        Culture backendDefaultCulture = serviceProvider.GetService<ICultureManager>().GetBackendDefaultCulture();

        if (backendDefaultCulture == null)
        {
          requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(DefaultCulture.Code);
          requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
            new CultureInfo[] { new CultureInfo(DefaultCulture.Code) }.ToList();
        }

        else
        {
          requestLocalizationOptions.DefaultRequestCulture = new RequestCulture(backendDefaultCulture.Code);
          requestLocalizationOptions.SupportedCultures = requestLocalizationOptions.SupportedUICultures =
            new CultureInfo[] { new CultureInfo(backendDefaultCulture.Code) }.ToList();
        }
      }

      applicationBuilder.UseWhen(
        context => context.Request.Path.StartsWithSegments(new PathString("/backend")),
        backendApplicationBuilder =>
        {
          backendApplicationBuilder.UseRequestLocalization(requestLocalizationOptions);
        }
      );
    }
  }
}