// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.ECommerce.Backend.ViewModels.Orders;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseOrdersPermission)]
  public class OrdersController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public OrdersController(IStorage storage)
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
        Order order = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(order);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IOrderRepository>().Create(order);

        else this.Storage.GetRepository<IOrderRepository>().Edit(order);

        this.Storage.Save();

        if (createOrEdit.Id == null)
          Event<IOrderCreatedEventHandler, IRequestHandler, Order>.Broadcast(this, order);

        else Event<IOrderEditedEventHandler, IRequestHandler, Order>.Broadcast(this, order);

        return this.Redirect(this.Request.CombineUrl("/backend/orders"));
      }

      return this.CreateRedirectToSelfResult();
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
      return this.View(new DetailsViewModelFactory(this).Create(id));
    }

    public ActionResult Delete(int id)
    {
      Order order = this.Storage.GetRepository<IOrderRepository>().WithKey(id);

      this.Storage.GetRepository<IOrderRepository>().Delete(order);
      this.Storage.Save();
      Event<IOrderDeletedEventHandler, IRequestHandler, Order>.Broadcast(this, order);
      return this.RedirectToAction("Index");
    }
  }
}