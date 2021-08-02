// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Backend.ViewModels.OrderStates;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageOrderStatesPermission)]
  public class OrderStatesController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, OrderState, OrderStateFilter> Repository
    {
      get => this.Storage.GetRepository<int, OrderState, OrderStateFilter>();
    }

    public OrderStatesController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]OrderStateFilter filter = null, string orderBy = "+position", int skip = 0, int take = 10)
    {
      return this.View(IndexViewModelFactory.Create(
        this.HttpContext, filter,
        await this.Repository.GetAllAsync(
          filter, orderBy, skip, take,
          new Inclusion<OrderState>(os => os.Name.Localizations)
        ),
        orderBy, skip, take, await this.Repository.CountAsync(filter)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(CreateOrEditViewModelFactory.Create(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync(
          (int)id, new Inclusion<OrderState>(os => os.Name.Localizations)
        )
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
    {
      if (createOrEdit.Id == null && !await this.IsCodeUniqueAsync(createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        OrderState orderState = CreateOrEditViewModelMapper.Map(
          createOrEdit.Id == null ? new OrderState() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        await this.CreateOrEditEntityLocalizationsAsync(orderState);

        if (createOrEdit.Id == null)
          this.Repository.Create(orderState);

        else this.Repository.Edit(orderState);

        await this.Storage.SaveAsync();

        if (createOrEdit.Id == null)
          Event<IOrderStateCreatedEventHandler, HttpContext, OrderState>.Broadcast(this.HttpContext, orderState);

        else Event<IOrderStateEditedEventHandler, HttpContext, OrderState>.Broadcast(this.HttpContext, orderState);

        return this.Redirect(this.Request.CombineUrl("/backend/orderstates"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<ActionResult> DeleteAsync(int id)
    {
      OrderState orderState = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(orderState.Id);
      await this.Storage.SaveAsync();
      Event<IOrderStateCreatedEventHandler, HttpContext, OrderState>.Broadcast(this.HttpContext, orderState);
      return this.Redirect(this.Request.CombineUrl("/backend/orderstates"));
    }

    private async Task<bool> IsCodeUniqueAsync(string code)
    {
      return await this.Repository.CountAsync(new OrderStateFilter(code: code)) == 0;
    }
  }
}