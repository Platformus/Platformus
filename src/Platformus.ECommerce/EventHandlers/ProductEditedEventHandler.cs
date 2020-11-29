// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;

namespace Platformus.ECommerce.EventHandlers
{
  public class ProductEditedEventHandler : IProductEditedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(HttpContext httpContext, Product product)
    {
    }
  }
}