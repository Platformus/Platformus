// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

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

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageOrdersPermission)]
  public class OrdersController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, Order, OrderFilter> Repository
    {
      get => this.Storage.GetRepository<int, Order, OrderFilter>();
    }

    public OrdersController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]OrderFilter filter = null, string orderBy = "-created", int skip = 0, int take = 10)
    {
      return this.View(await IndexViewModelFactory.CreateAsync(
        this.HttpContext, filter,
        await this.Repository.GetAllAsync(
          filter, orderBy, skip, take,
          new Inclusion<Order>(o => o.OrderState.Name.Localizations),
          new Inclusion<Order>(o => o.DeliveryMethod.Name.Localizations),
          new Inclusion<Order>(o => o.PaymentMethod.Name.Localizations),
          new Inclusion<Order>(o => o.Positions)
        ),
        orderBy, skip, take, await this.Repository.CountAsync(filter)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(await CreateOrEditViewModelFactory.CreateAsync(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync(
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
          createOrEdit.Id == null ? new Order() : await this.Repository.GetByIdAsync((int)createOrEdit.Id, new Inclusion<Order>("Positions")),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(order);

        else this.Repository.Edit(order);

        await this.Storage.SaveAsync();
        await this.CreateOrEditPositionsAsync(order, createOrEdit);

        if (createOrEdit.Id == null)
          Event<IOrderCreatedEventHandler, HttpContext, Order>.Broadcast(this.HttpContext, order);

        else Event<IOrderEditedEventHandler, HttpContext, Order>.Broadcast(this.HttpContext, order);

        return this.Redirect(this.Request.CombineUrl("/backend/orders"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<ActionResult> DeleteAsync(int id)
    {
      Order order = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(order.Id);
      await this.Storage.SaveAsync();
      Event<IOrderCreatedEventHandler, HttpContext, Order>.Broadcast(this.HttpContext, order);
      return this.Redirect(this.Request.CombineUrl("/backend/orders"));
    }

    private async Task CreateOrEditPositionsAsync(Order order, CreateOrEditViewModel createOrEdit)
    {
      await this.DeletePositionsAsync(order);
      await this.CreatePositionsAsync(order, createOrEdit);
    }

    private async Task DeletePositionsAsync(Order order)
    {
      if (order.Positions != null)
        foreach (Position position in order.Positions)
          this.Storage.GetRepository<int, Position, PositionFilter>().Delete(position.Id);

      await this.Storage.SaveAsync();
    }

    private async Task CreatePositionsAsync(Order order, CreateOrEditViewModel createOrEdit)
    {
      foreach (PositionViewModel positionViewModel in createOrEdit.Positions)
      {
        Position position = new Position();

        position.OrderId = order.Id;
        position.ProductId = positionViewModel.Product.Id;
        position.Price = positionViewModel.Price;
        position.Quantity = positionViewModel.Quantity;
        position.Subtotal = position.GetSubtotal();
        this.Storage.GetRepository<int, Position, PositionFilter>().Create(position);
      }

      await this.Storage.SaveAsync();
    }
  }
}