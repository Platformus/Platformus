// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.WebApplication.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Platformus.Core.Services.Abstractions;

namespace Platformus.WebApplication.Extensions
{
  public static class ApplicationBuilderExtensions
  {
    public static void UsePlatformus(this IApplicationBuilder applicationBuilder)
    {
      applicationBuilder.UseExtCore();

      IHostApplicationLifetime hostApplicationLifetime = applicationBuilder.ApplicationServices.GetService<IHostApplicationLifetime>();

      hostApplicationLifetime.ApplicationStarted.Register(() => {
        ICleaningManager cleaningManager = applicationBuilder.ApplicationServices.GetService<ICleaningManager>();

        cleaningManager.CleanupAsync(applicationBuilder.ApplicationServices);
      });
    }
  }
}