// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.ECommerce.Backend.ViewModels.Carts;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseCartsPermission)]
  public class CartsController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public CartsController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string orderBy = "created", string direction = "desc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(orderBy, direction, skip, take, filter));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public IActionResult CreateOrEdit(int? id)
    {
      return this.View(new CreateOrEditViewModelFactory(this).Create(id));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public IActionResult CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Cart cart = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(cart);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<ICartRepository>().Create(cart);

        else this.Storage.GetRepository<ICartRepository>().Edit(cart);

        this.Storage.Save();

        if (createOrEdit.Id == null)
          Event<ICartCreatedEventHandler, IRequestHandler, Cart>.Broadcast(this, cart);

        else Event<ICartEditedEventHandler, IRequestHandler, Cart>.Broadcast(this, cart);

        return this.Redirect(this.Request.CombineUrl("/backend/carts"));
      }

      return this.CreateRedirectToSelfResult();
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
      return this.View(new DetailsViewModelFactory(this).Create(id));
    }

    //[HttpPost]
    //public IActionResult AddProduct(int cartId, int productId)
    //{
    //  Position position = new Position();

    //  position.CartId = cartId;
    //  position.ProductId = productId;
    //  this.Storage.GetRepository<IPositionRepository>().Create(position);
    //  this.Storage.Save();
    //  return this.Ok();
    //}

    public IActionResult Delete(int id)
    {
      Cart cart = this.Storage.GetRepository<ICartRepository>().WithKey(id);

      this.Storage.GetRepository<ICartRepository>().Delete(cart);
      this.Storage.Save();
      Event<ICartDeletedEventHandler, IRequestHandler, Cart>.Broadcast(this, cart);
      return this.RedirectToAction("Index");
    }
  }
}