// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Platformus.Forms.Frontend.Actions
{
  public class UseMvcAction : IUseMvcAction
  {
    public int Priority => 1000;

    public void Execute(IRouteBuilder routeBuilder, IServiceProvider serviceProvider)
    {
      routeBuilder.MapRoute(name: "Forms", template: "{culture=en}/forms/send", defaults: new { controller = "Forms", action = "Send" });
    }
  }
}