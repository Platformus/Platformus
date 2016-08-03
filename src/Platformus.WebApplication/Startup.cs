// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Platformus.WebApplication
{
  public abstract class Startup : ExtCore.WebApplication.Startup
  {
    public Startup(IServiceProvider serviceProvider)
      : base(serviceProvider)
    {
      IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(this.serviceProvider.GetService<IHostingEnvironment>().ContentRootPath)
        .AddJsonFile("config.json", optional: true, reloadOnChange: true);

      this.configurationRoot = configurationBuilder.Build();
    }

    public override void ConfigureServices(IServiceCollection services)
    {
      base.ConfigureServices(services);
    }

    public override void Configure(IApplicationBuilder applicationBuilder)
    {
      if (this.serviceProvider.GetService<IHostingEnvironment>().IsEnvironment("Development"))
      {
        applicationBuilder.UseBrowserLink();
        applicationBuilder.UseDeveloperExceptionPage();
        applicationBuilder.UseDatabaseErrorPage();
      }

      else
      {

      }

      base.Configure(applicationBuilder);
    }
  }
}