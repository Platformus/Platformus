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
using Platformus.ECommerce.Backend.ViewModels.DeliveryMethods;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;
using Platformus.ECommerce.Filters;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Authorize(Policy = Policies.HasManageDeliveryMethodsPermission)]
  public class DeliveryMethodsController : Core.Backend.Controllers.ControllerBase
  {
    private IStringLocalizer localizer;

    private IRepository<int, DeliveryMethod, DeliveryMethodFilter> Repository
    {
      get => this.Storage.GetRepository<int, DeliveryMethod, DeliveryMethodFilter>();
    }

    public DeliveryMethodsController(IStorage storage, IStringLocalizer<SharedResource> localizer)
      : base(storage)
    {
      this.localizer = localizer;
    }

    public async Task<IActionResult> IndexAsync([FromQuery]DeliveryMethodFilter filter = null, string sorting = "+position", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(
        sorting, offset, limit, await this.Repository.CountAsync(filter),
        await this.Repository.GetAllAsync(filter, sorting, offset, limit, new Inclusion<DeliveryMethod>(dm => dm.Name.Localizations))
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(CreateOrEditViewModelFactory.Create(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync(
          (int)id, new Inclusion<DeliveryMethod>(dm => dm.Name.Localizations)
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
        DeliveryMethod deliveryMethod = CreateOrEditViewModelMapper.Map(
          createOrEdit.Id == null ?
            new DeliveryMethod() :
            await this.Repository.GetByIdAsync(
              (int)createOrEdit.Id,
              new Inclusion<DeliveryMethod>(dm => dm.Name.Localizations)
            ),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(deliveryMethod);

        else this.Repository.Edit(deliveryMethod);

        await this.MergeEntityLocalizationsAsync(deliveryMethod);
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
      return this.Redirect(this.Request.CombineUrl("/backend/deliverymethods"));
    }

    private async Task<bool> IsCodeUniqueAsync(CreateOrEditViewModel createOrEdit)
    {
      DeliveryMethod deliveryMethod = (await this.Repository.GetAllAsync(new DeliveryMethodFilter(code: createOrEdit.Code))).FirstOrDefault();

      return deliveryMethod == null || deliveryMethod.Id == createOrEdit.Id;
    }
  }
}