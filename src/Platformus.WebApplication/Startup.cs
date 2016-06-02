// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Platformus.WebApplication
{
  public class Startup : ExtCore.WebApplication.Startup
  {
    public Startup(IHostingEnvironment hostingEnvironment)
      : base(hostingEnvironment)
    {
      IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(hostingEnvironment.ContentRootPath)
        .AddJsonFile("config.json", optional: true, reloadOnChange: true);

      this.configurationRoot = configurationBuilder.Build();
    }

    public override void ConfigureServices(IServiceCollection services)
    {
      base.ConfigureServices(services);
      services.AddSession();
    }

    public override void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
    {
      applicationBuilder.UseSession();

      if (hostingEnvironment.IsEnvironment("Development"))
      {
        applicationBuilder.UseBrowserLink();
        applicationBuilder.UseDeveloperExceptionPage();
        applicationBuilder.UseDatabaseErrorPage();
      }

      else
      {

      }

      base.Configure(applicationBuilder, hostingEnvironment);
    }
  }
}