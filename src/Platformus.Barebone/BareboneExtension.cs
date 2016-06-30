// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Infrastructure;

namespace Platformus.Barebone
{
  public class BareboneExtension : IExtension
  {
    private IConfigurationRoot configurationRoot;

    public string Name
    {
      get
      {
        return "Barebone Extension";
      }
    }

    public IDictionary<int, Action<IRouteBuilder>> RouteRegistrarsByPriorities
    {
      get
      {
        Dictionary<int, Action<IRouteBuilder>> routeRegistrarsByPriorities = new Dictionary<int, Action<IRouteBuilder>>();

        routeRegistrarsByPriorities.Add(
          1000,
          routeBuilder =>
          {
            routeBuilder.MapRoute(name: "Backend Create", template: "{area:exists}/{controller=Default}/create", defaults: new { action = "CreateOrEdit" });
            routeBuilder.MapRoute(name: "Backend Edit", template: "{area:exists}/{controller=Default}/edit/{id}", defaults: new { action = "CreateOrEdit" });
            routeBuilder.MapRoute(name: "Backend Default", template: "{area:exists}/{controller=Default}/{action=Index}/{id?}");
          }
        );

        return routeRegistrarsByPriorities;
      }
    }

    public IFrontendMetadata FrontendMetadata
    {
      get
      {
        return null;
      }
    }

    public IBackendMetadata BackendMetadata
    {
      get
      {
        return new BackendMetadata();
      }
    }

    public void SetConfigurationRoot(IConfigurationRoot configurationRoot)
    {
      this.configurationRoot = configurationRoot;
    }

    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
    }
  }
}