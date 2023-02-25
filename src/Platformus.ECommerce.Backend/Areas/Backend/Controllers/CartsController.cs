// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Backend.ViewModels.Carts;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.Controllers;

[Authorize(Policy = Policies.HasManageCartsPermission)]
public class CartsController : Core.Backend.Controllers.ControllerBase
{
  private IRepository<int, Cart, CartFilter> Repository
  {
    get => this.Storage.GetRepository<int, Cart, CartFilter>();
  }

  public CartsController(IStorage storage)
    : base(storage)
  {
  }

  public async Task<IActionResult> IndexAsync([FromQuery] CartFilter filter = null, string sorting = "-created", int offset = 0, int limit = 10)
  {
    return this.View(IndexViewModelFactory.Create(
      sorting, offset, limit, await this.Repository.CountAsync(filter),
      await this.Repository.GetAllAsync(filter, sorting, offset, limit, new Inclusion<Cart>(c => c.Positions))
    ));
  }

  [HttpGet]
  public async Task<IActionResult> ViewAsync(int id)
  {
    return this.View(ViewViewModelFactory.Create(
      await this.Repository.GetByIdAsync(
        id,
        new Inclusion<Cart>("Positions.Product.Category.Name.Localizations"),
        new Inclusion<Cart>("Positions.Product.Name.Localizations")
      )
    ));
  }

  public async Task<ActionResult> DeleteAsync(int id)
  {
    Cart cart = await this.Repository.GetByIdAsync(id);

    this.Repository.Delete(cart.Id);
    await this.Storage.SaveAsync();
    Event<ICartCreatedEventHandler, HttpContext, Cart>.Broadcast(this.HttpContext, cart);
    return this.Redirect(this.Request.CombineUrl("/backend/carts"));
  }
}