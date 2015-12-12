// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Infrastructure;

namespace Platformus.Navigation
{
  public class NavigationExtension : IExtension
  {
    private IConfigurationRoot configurationRoot;

    public string Name
    {
      get
      {
        return "Navigation Extension";
      }
    }

    public IEnumerable<BackendMenuGroup> BackendMenuGroups
    {
      get
      {
        return new BackendMenuGroup[]
        {
           new BackendMenuGroup(
            "Content",
            1000,
            new BackendMenuItem[]
            {
              new BackendMenuItem("/backend/menus", "Menus", 2000)
            }
          )
        };
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

    public void RegisterRoutes(IRouteBuilder routeBuilder)
    {
    }
  }
}