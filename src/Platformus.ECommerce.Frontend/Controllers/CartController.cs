// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Frontend.ViewModels.Cart;

namespace Platformus.ECommerce.Frontend.Controllers
{
  [AllowAnonymous]
  public class CartController : Platformus.Barebone.Frontend.Controllers.ControllerBase
  {
    public CartController(IStorage storage)
      : base(storage)
    {
    }

    [HttpGet]
    public IActionResult Index()
    {
      return this.View(new IndexViewModelFactory(this).Create());
    }

    [HttpGet]
    public IActionResult Add(int productId)
    {
      new CartManager(this).Add(productId);
      return this.RedirectToAction("index");
    }

    [HttpGet]
    public IActionResult Remove(int productId)
    {
      new CartManager(this).Remove(productId);
      return this.RedirectToAction("index");
    }
  }
}