// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Platformus.WebApplication
{
  public class Startup : ExtCore.WebApplication.Startup
  {
    public Startup(IHostingEnvironment hostingEnvironment, IApplicationEnvironment applicationEnvironment, IAssemblyLoaderContainer assemblyLoaderContainer, IAssemblyLoadContextAccessor assemblyLoadContextAccessor, ILibraryManager libraryManager)
      : base(hostingEnvironment, applicationEnvironment, assemblyLoaderContainer, assemblyLoadContextAccessor, libraryManager)
    {
      IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
        .AddJsonFile("config.json");

      this.configurationRoot = configurationBuilder.Build();
    }

    public override void ConfigureServices(IServiceCollection services)
    {
      base.ConfigureServices(services);
    }

    public override void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
    {
      if (hostingEnvironment.IsEnvironment("Development"))
      {
        applicationBuilder.UseBrowserLink();
        applicationBuilder.UseDeveloperExceptionPage();
        applicationBuilder.UseDatabaseErrorPage();
      }

      else
      {
        applicationBuilder.UseExceptionHandler("/");
      }

      base.Configure(applicationBuilder, hostingEnvironment);
    }
  }
}