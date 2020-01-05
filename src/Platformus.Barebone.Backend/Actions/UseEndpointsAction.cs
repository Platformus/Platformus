// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Platformus.Barebone.Backend.Actions
{
  public class UseEndpointsAction : IUseEndpointsAction
  {
    public int Priority => 1000;

    public void Execute(IEndpointRouteBuilder endpointRouteBuilder, IServiceProvider serviceProvider)
    {
      endpointRouteBuilder.MapControllerRoute(name: "Backend Create", pattern: "{area:exists}/{controller=Default}/create", defaults: new { action = "CreateOrEdit" });
      endpointRouteBuilder.MapControllerRoute(name: "Backend Edit", pattern: "{area:exists}/{controller=Default}/edit/{id}", defaults: new { action = "CreateOrEdit" });
      endpointRouteBuilder.MapControllerRoute(name: "Backend Default", pattern: "{area:exists}/{controller=Default}/{action=Index}/{id?}");
    }
  }
}