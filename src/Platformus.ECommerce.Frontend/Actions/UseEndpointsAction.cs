// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Platformus.ECommerce.Frontend.Actions
{
  public class UseEndpointsAction : IUseEndpointsAction
  {
    public int Priority => 1000;

    public void Execute(IEndpointRouteBuilder endpointRouteBuilder, IServiceProvider serviceProvider)
    {
      endpointRouteBuilder.MapControllerRoute(name: "Filter", pattern: "{culture=en}/ecommerce/filter", defaults: new { controller = "Filter", action = "Index" });
      endpointRouteBuilder.MapControllerRoute(name: "Cart", pattern: "{culture=en}/ecommerce/cart", defaults: new { controller = "Cart", action = "Index" });
      endpointRouteBuilder.MapControllerRoute(name: "Add To Cart", pattern: "{culture=en}/ecommerce/cart/add", defaults: new { controller = "Cart", action = "Add" });
      endpointRouteBuilder.MapControllerRoute(name: "Remove From Cart", pattern: "{culture=en}/ecommerce/cart/remove", defaults: new { controller = "Cart", action = "Remove" });
      endpointRouteBuilder.MapControllerRoute(name: "Checkout", pattern: "{culture=en}/ecommerce/checkout", defaults: new { controller = "Checkout", action = "Index" });
      endpointRouteBuilder.MapControllerRoute(name: "Done", pattern: "{culture=en}/ecommerce/checkout/done", defaults: new { controller = "Checkout", action = "Done" });
    }
  }
}