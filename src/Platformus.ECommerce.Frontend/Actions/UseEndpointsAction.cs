// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
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
      endpointRouteBuilder.MapControllerRoute(name: "Add to Cart", pattern: "{culture}/cart/add-to-cart", defaults: new { controller = "Cart", action = "AddToCart" });
      endpointRouteBuilder.MapControllerRoute(name: "Remove from Cart", pattern: "{culture}/cart/remove-from-cart", defaults: new { controller = "Cart", action = "RemoveFromCart" });
      endpointRouteBuilder.MapControllerRoute(name: "Checkout", pattern: "{culture}/ecommerce/checkout", defaults: new { controller = "ECommerce", action = "Checkout" });
      endpointRouteBuilder.MapControllerRoute(name: "Thank You", pattern: "{culture}/ecommerce/thank-you/{orderId}", defaults: new { controller = "ECommerce", action = "ThankYou" });
    }
  }
}