// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Infrastructure;

namespace Platformus.Security
{
  public class SecurityExtension : IExtension
  {
    private IConfigurationRoot configurationRoot;

    public string Name
    {
      get
      {
        return "Forms Extension";
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
            routeBuilder.MapRoute(name: "Forms", template: "{culture=en}/forms/send", defaults: new { controller = "Forms", action = "Send" });
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