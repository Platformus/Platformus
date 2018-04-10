// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Backend.ViewModels.PaymentMethods;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowsePaymentMethodsPermission)]
  public class PaymentMethodsController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public PaymentMethodsController(IStorage storage)
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
        PaymentMethod paymentMethod = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(paymentMethod);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IPaymentMethodRepository>().Create(paymentMethod);

        else this.Storage.GetRepository<IPaymentMethodRepository>().Edit(paymentMethod);

        this.Storage.Save();

        return this.Redirect(this.Request.CombineUrl("/backend/paymentmethods"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      PaymentMethod paymentMethod = this.Storage.GetRepository<IPaymentMethodRepository>().WithKey(id);

      this.Storage.GetRepository<IPaymentMethodRepository>().Delete(paymentMethod);
      this.Storage.Save();
      return this.RedirectToAction("Index");
    }

    private bool IsCodeUnique(string code)
    {
      return this.Storage.GetRepository<IPaymentMethodRepository>().WithCode(code) == null;
    }
  }
}