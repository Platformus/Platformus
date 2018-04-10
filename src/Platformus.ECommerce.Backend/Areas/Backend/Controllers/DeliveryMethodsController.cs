// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Backend.ViewModels.DeliveryMethods;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseDeliveryMethodsPermission)]
  public class DeliveryMethodsController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public DeliveryMethodsController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string orderBy = "position", string direction = "asc", int skip = 0, int take = 10, string filter = null)
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
      if (createOrEdit.Id == null && !this.IsCodeUnique(createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        DeliveryMethod deliveryMethod = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(deliveryMethod);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IDeliveryMethodRepository>().Create(deliveryMethod);

        else this.Storage.GetRepository<IDeliveryMethodRepository>().Edit(deliveryMethod);

        this.Storage.Save();

        return this.Redirect(this.Request.CombineUrl("/backend/deliverymethods"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      DeliveryMethod deliveryMethod = this.Storage.GetRepository<IDeliveryMethodRepository>().WithKey(id);

      this.Storage.GetRepository<IDeliveryMethodRepository>().Delete(deliveryMethod);
      this.Storage.Save();
      return this.RedirectToAction("Index");
    }

    private bool IsCodeUnique(string code)
    {
      return this.Storage.GetRepository<IDeliveryMethodRepository>().WithCode(code) == null;
    }
  }
}