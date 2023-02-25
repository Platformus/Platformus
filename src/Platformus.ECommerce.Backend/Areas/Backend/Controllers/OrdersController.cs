// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Backend.ViewModels.Orders;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.Controllers;

[Authorize(Policy = Policies.HasManageOrdersPermission)]
public class OrdersController : Core.Backend.Controllers.ControllerBase
{
  private IRepository<int, Order, OrderFilter> OrderRepository
  {
    get => this.Storage.GetRepository<int, Order, OrderFilter>();
  }

  private IRepository<int, Position, PositionFilter> PositionRepository
  {
    get => this.Storage.GetRepository<int, Position, PositionFilter>();
  }

  public OrdersController(IStorage storage)
    : base(storage)
  {
  }

  public async Task<IActionResult> IndexAsync([FromQuery] OrderFilter filter = null, string sorting = "-created", int offset = 0, int limit = 10)
  {
    return this.View(await IndexViewModelFactory.CreateAsync(
      this.HttpContext, sorting, offset, limit, await this.OrderRepository.CountAsync(filter),
      await this.OrderRepository.GetAllAsync(
        filter, sorting, offset, limit,
        new Inclusion<Order>(o => o.OrderState.Name.Localizations),
        new Inclusion<Order>(o => o.DeliveryMethod.Name.Localizations),
        new Inclusion<Order>(o => o.PaymentMethod.Name.Localizations),
        new Inclusion<Order>(o => o.Positions)
      )
    ));
  }

  [HttpGet]
  [ImportModelStateFromTempData]
  public async Task<IActionResult> CreateOrEditAsync(int? id)
  {
    return this.View(await CreateOrEditViewModelFactory.CreateAsync(
      this.HttpContext, id == null ? null : await this.OrderRepository.GetByIdAsync(
        (int)id,
        new Inclusion<Order>(o => o.OrderState.Name.Localizations),
        new Inclusion<Order>(o => o.DeliveryMethod.Name.Localizations),
        new Inclusion<Order>(o => o.PaymentMethod.Name.Localizations),
        new Inclusion<Order>("Positions.Product.Category.Name.Localizations"),
        new Inclusion<Order>("Positions.Product.Name.Localizations")
      )
    ));
  }

  [HttpPost]
  [ExportModelStateToTempData]
  public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
  {
    if (this.ModelState.IsValid)
    {
      Order order = CreateOrEditViewModelMapper.Map(
        createOrEdit.Id == null ? new Order() { Positions = new List<Position>() } : await this.OrderRepository.GetByIdAsync((int)createOrEdit.Id, new Inclusion<Order>("Positions")),
        createOrEdit
      );

      if (createOrEdit.Id == null)
        this.OrderRepository.Create(order);

      else this.OrderRepository.Edit(order);

      this.MergePositions(order, createOrEdit);
      await this.Storage.SaveAsync();

      if (createOrEdit.Id == null)
        Event<IOrderCreatedEventHandler, HttpContext, Order>.Broadcast(this.HttpContext, order);

      else Event<IOrderEditedEventHandler, HttpContext, Order>.Broadcast(this.HttpContext, order);

      return this.Redirect(this.Request.CombineUrl("/backend/orders"));
    }

    return this.CreateRedirectToSelfResult();
  }

  [HttpPost]
  public IActionResult Positions([FromBody] IEnumerable<PositionViewModel> positions)
  {
    return this.PartialView("_Positions", positions);
  }

  public async Task<ActionResult> DeleteAsync(int id)
  {
    Order order = await this.OrderRepository.GetByIdAsync(id);

    this.OrderRepository.Delete(order.Id);
    await this.Storage.SaveAsync();
    Event<IOrderCreatedEventHandler, HttpContext, Order>.Broadcast(this.HttpContext, order);
    return this.Redirect(this.Request.CombineUrl("/backend/orders"));
  }

  private void MergePositions(Order order, CreateOrEditViewModel createOrEdit)
  {
    IEnumerable<Position> currentPositions = order.Positions;

    foreach (Position currentPosition in currentPositions.Where(cp => !createOrEdit.Positions.Any(p => p.Id == cp.Id)))
      this.PositionRepository.Delete(currentPosition.Id);

    foreach (Position currentPosition in currentPositions)
    {
      PositionViewModel position = createOrEdit.Positions.FirstOrDefault(p => p.Id == currentPosition.Id);

      if (position != null)
      {
        currentPosition.Price = position.Price;
        currentPosition.Quantity = position.Quantity;
        this.PositionRepository.Edit(currentPosition);
      }
    }

    foreach (Position position in createOrEdit.Positions.Where(p => p.Id == 0).Select(p => new Position() { Order = order, ProductId = p.Product.Id, Price = p.Price, Quantity = p.Quantity }))
      this.PositionRepository.Create(position);
  }
}