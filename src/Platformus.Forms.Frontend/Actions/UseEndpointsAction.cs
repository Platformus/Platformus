// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Platformus.Forms.Frontend.Actions
{
  public class UseEndpointsAction : IUseEndpointsAction
  {
    public int Priority => 1000;

    public void Execute(IEndpointRouteBuilder endpointRouteBuilder, IServiceProvider serviceProvider)
    {
      endpointRouteBuilder.MapControllerRoute(name: "Forms", pattern: "{culture=en}/forms/send", defaults: new { controller = "Forms", action = "Send" });
    }
  }
}