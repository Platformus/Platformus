// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Infrastructure;

namespace Platformus.Barebone
{
  public class BareboneExtension : ExtensionBase
  {
    public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
    {
      get
      {
        return new Dictionary<int, Action<IServiceCollection>>()
        {
          [1000] = services =>
          {
            services.AddMemoryCache();
            services.AddSingleton(typeof(ICache), typeof(DefaultCache));
            services.AddSession();
          }
        };
      }
    }

    public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
    {
      get
      {
        return new Dictionary<int, Action<IApplicationBuilder>>()
        {
          [1000] = applicationBuilder =>
          {
            applicationBuilder.UseHttpException();
            applicationBuilder.UseSession();
          }
        };
      }
    }

    public override IEnumerable<KeyValuePair<int, Action<IRouteBuilder>>> UseMvcActionsByPriorities
    {
      get
      {
        return new Dictionary<int, Action<IRouteBuilder>>()
        {
          [1000] = routeBuilder =>
          {
            routeBuilder.MapRoute(name: "Backend Create", template: "{area:exists}/{controller=Default}/create", defaults: new { action = "CreateOrEdit" });
            routeBuilder.MapRoute(name: "Backend Edit", template: "{area:exists}/{controller=Default}/edit/{id}", defaults: new { action = "CreateOrEdit" });
            routeBuilder.MapRoute(name: "Backend Default", template: "{area:exists}/{controller=Default}/{action=Index}/{id?}");
          }
        };
      }
    }

    public override IBackendMetadata BackendMetadata
    {
      get
      {
        return new BackendMetadata();
      }
    }
  }
}