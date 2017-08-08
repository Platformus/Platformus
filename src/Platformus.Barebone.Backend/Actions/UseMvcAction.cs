// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Platformus.Barebone.Backend.Actions
{
  public class UseMvcAction : IUseMvcAction
  {
    public int Priority => 1000;

    public void Execute(IRouteBuilder routeBuilder, IServiceProvider serviceProvider)
    {
      routeBuilder.MapRoute(name: "Backend Create", template: "{area:exists}/{controller=Default}/create", defaults: new { action = "CreateOrEdit" });
      routeBuilder.MapRoute(name: "Backend Edit", template: "{area:exists}/{controller=Default}/edit/{id}", defaults: new { action = "CreateOrEdit" });
      routeBuilder.MapRoute(name: "Backend Default", template: "{area:exists}/{controller=Default}/{action=Index}/{id?}");
    }
  }
}