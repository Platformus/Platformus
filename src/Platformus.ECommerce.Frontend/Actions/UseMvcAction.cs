// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Platformus.ECommerce.Frontend.Actions
{
  public class UseMvcAction : IUseMvcAction
  {
    public int Priority => 1000;

    public void Execute(IRouteBuilder routeBuilder, IServiceProvider serviceProvider)
    {
      routeBuilder.MapRoute(name: "Cart", template: "{culture=en}/ecommerce/cart", defaults: new { controller = "Cart", action = "Index" });
      routeBuilder.MapRoute(name: "Add To Cart", template: "{culture=en}/ecommerce/cart/add", defaults: new { controller = "Cart", action = "Add" });
      routeBuilder.MapRoute(name: "Remove From Cart", template: "{culture=en}/ecommerce/cart/remove", defaults: new { controller = "Cart", action = "Remove" });
      routeBuilder.MapRoute(name: "Checkout", template: "{culture=en}/ecommerce/checkout", defaults: new { controller = "Checkout", action = "Index" });
      routeBuilder.MapRoute(name: "Done", template: "{culture=en}/ecommerce/checkout/done", defaults: new { controller = "Checkout", action = "Done" });
    }
  }
}