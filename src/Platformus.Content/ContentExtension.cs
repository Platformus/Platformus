// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Infrastructure;

namespace Platformus.Content
{
  public class ContentExtension : IExtension
  {
    private IConfigurationRoot configurationRoot;

    public string Name
    {
      get
      {
        return "Content Extension";
      }
    }

    public IDictionary<int, Action<IRouteBuilder>> RouteRegistrarsByPriorities
    {
      get
      {
        Dictionary<int, Action<IRouteBuilder>> routeRegistrarsByPriorities = new Dictionary<int, Action<IRouteBuilder>>();

        routeRegistrarsByPriorities.Add(
          10000,
          routeBuilder =>
          {
            routeBuilder.MapRoute(name: "Default", template: "{culture=en}/{*url}", defaults: new { controller = "Default", action = "Index" });
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