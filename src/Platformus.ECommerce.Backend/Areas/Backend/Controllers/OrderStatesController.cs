// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend;
using Platformus.ECommerce.Backend.ViewModels.OrderStates;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Authorize(Policy = Policies.HasManageOrderStatesPermission)]
  public class OrderStatesController : Core.Backend.Controllers.ControllerBase
  {
    private IStringLocalizer localizer;

    private IRepository<int, OrderState, OrderStateFilter> Repository
    {
      get => this.Storage.GetRepository<int, OrderState, OrderStateFilter>();
    }

    public OrderStatesController(IStorage storage, IStringLocalizer<SharedResource> localizer)
      : base(storage)
    {
      this.localizer = localizer;
    }

    public async Task<IActionResult> IndexAsync([FromQuery]OrderStateFilter filter = null, string sorting = "+position", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(
        sorting, offset, limit, await this.Repository.CountAsync(filter),
        await this.Repository.GetAllAsync(filter, sorting, offset, limit, new Inclusion<OrderState>(os => os.Name.Localizations))
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
      if (!await this.IsCodeUniqueAsync(createOrEdit))
        this.ModelState.AddModelError("code", this.localizer["Value is already in use"]);

      if (this.ModelState.IsValid)
      {
        OrderState orderState = CreateOrEditViewModelMapper.Map(
          createOrEdit.Id == null ?
            new OrderState() :
            await this.Repository.GetByIdAsync(
              (int)createOrEdit.Id,
              new Inclusion<OrderState>(os => os.Name.Localizations)
            ),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(orderState);

        else this.Repository.Edit(orderState);

        await this.MergeEntityLocalizationsAsync(orderState);
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

    private async Task<bool> IsCodeUniqueAsync(CreateOrEditViewModel createOrEdit)
    {
      OrderState orderState = (await this.Repository.GetAllAsync(new OrderStateFilter(code: createOrEdit.Code))).FirstOrDefault();

      return orderState == null || orderState.Id == createOrEdit.Id;
    }
  }
}