// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Backend.ViewModels.DeliveryMethods;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageDeliveryMethodsPermission)]
  public class DeliveryMethodsController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, DeliveryMethod, DeliveryMethodFilter> Repository
    {
      get => this.Storage.GetRepository<int, DeliveryMethod, DeliveryMethodFilter>();
    }

    public DeliveryMethodsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]DeliveryMethodFilter filter = null, string orderBy = "+position", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelFactory().Create(
        this.HttpContext, filter,
        await this.Repository.GetAllAsync(
          filter, orderBy, skip, take,
          new Inclusion<DeliveryMethod>(dm => dm.Name.Localizations)
        ),
        orderBy, skip, take, await this.Repository.CountAsync(filter)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(new CreateOrEditViewModelFactory().Create(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync(
          (int)id, new Inclusion<DeliveryMethod>(dm => dm.Name.Localizations)
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
        DeliveryMethod deliveryMethod = new CreateOrEditViewModelMapper().Map(
          createOrEdit.Id == null ? new DeliveryMethod() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        await this.CreateOrEditEntityLocalizationsAsync(deliveryMethod);

        if (createOrEdit.Id == null)
          this.Repository.Create(deliveryMethod);

        else this.Repository.Edit(deliveryMethod);

        await this.Storage.SaveAsync();

        if (createOrEdit.Id == null)
          Event<IDeliveryMethodCreatedEventHandler, HttpContext, DeliveryMethod>.Broadcast(this.HttpContext, deliveryMethod);

        else Event<IDeliveryMethodEditedEventHandler, HttpContext, DeliveryMethod>.Broadcast(this.HttpContext, deliveryMethod);

        return this.Redirect(this.Request.CombineUrl("/backend/deliverymethods"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<ActionResult> DeleteAsync(int id)
    {
      DeliveryMethod deliveryMethod = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(deliveryMethod.Id);
      await this.Storage.SaveAsync();
      Event<IDeliveryMethodCreatedEventHandler, HttpContext, DeliveryMethod>.Broadcast(this.HttpContext, deliveryMethod);
      return this.RedirectToAction("Index");
    }

    private async Task<bool> IsCodeUniqueAsync(string code)
    {
      return await this.Repository.CountAsync(new DeliveryMethodFilter() { Code = code }) == 0;
    }
  }
}