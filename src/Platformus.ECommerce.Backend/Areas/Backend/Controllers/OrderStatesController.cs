// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Backend.ViewModels.OrderStates;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseOrderStatesPermission)]
  public class OrderStatesController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public OrderStatesController(IStorage storage)
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
        OrderState orderState = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(orderState);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IOrderStateRepository>().Create(orderState);

        else this.Storage.GetRepository<IOrderStateRepository>().Edit(orderState);

        this.Storage.Save();

        return this.Redirect(this.Request.CombineUrl("/backend/orderstates"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      OrderState orderState = this.Storage.GetRepository<IOrderStateRepository>().WithKey(id);

      this.Storage.GetRepository<IOrderStateRepository>().Delete(orderState);
      this.Storage.Save();
      return this.RedirectToAction("Index");
    }

    private bool IsCodeUnique(string code)
    {
      return this.Storage.GetRepository<IOrderStateRepository>().WithCode(code) == null;
    }
  }
}