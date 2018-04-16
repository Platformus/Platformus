// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;
using Platformus.ECommerce.Frontend.ViewModels.Checkout;
using Platformus.Globalization.Frontend;

namespace Platformus.ECommerce.Frontend.Controllers
{
  [AllowAnonymous]
  public class CheckoutController : Platformus.Barebone.Frontend.Controllers.ControllerBase
  {
    public CheckoutController(IStorage storage)
      : base(storage)
    {
    }

    [HttpGet]
    public IActionResult Index()
    {
      return this.View();
    }

    [HttpPost]
    public IActionResult Index(IndexViewModel indexViewModel)
    {
      if (this.ModelState.IsValid)
      {
        Order order = new IndexViewModelMapper(this).Map(indexViewModel);

        this.Storage.GetRepository<IOrderRepository>().Create(order);
        this.Storage.Save();
        new CartManager(this).AssignTo(order);
        Event<IOrderCreatedEventHandler, IRequestHandler, Order>.Broadcast(this, order);
        return this.Redirect(GlobalizedUrlFormatter.Format(this, "/ecommerce/checkout/done?orderid=" + order.Id));
      }

      return this.View(indexViewModel);
    }

    [HttpGet]
    public IActionResult Done(int orderId)
    {
      return this.View(orderId);
    }
  }
}