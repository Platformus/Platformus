// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Platformus.ECommerce.Frontend.Middleware;

namespace Platformus.ECommerce.Frontend.Actions
{
  public class UseWebsiteMiddlewareConfigureAction : IConfigureAction
  {
    public int Priority => 2000;

    public void Execute(IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
    {
      applicationBuilder.UseMiddleware<ECommerceMiddleware>();
    }
  }
}