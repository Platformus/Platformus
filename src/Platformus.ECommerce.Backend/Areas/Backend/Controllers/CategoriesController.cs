// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.ECommerce.Backend.ViewModels.Categories;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseCategoriesPermission)]
  public class CategoriesController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public CategoriesController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index()
    {
      return this.View(new IndexViewModelFactory(this).Create());
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
        Category category = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(category);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<ICategoryRepository>().Create(category);

        else this.Storage.GetRepository<ICategoryRepository>().Edit(category);

        this.Storage.Save();

        if (createOrEdit.Id == null)
          Event<ICategoryCreatedEventHandler, IRequestHandler, Category>.Broadcast(this, category);

        else Event<ICategoryEditedEventHandler, IRequestHandler, Category>.Broadcast(this, category);

        return this.RedirectToAction("Index");
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Category category = this.Storage.GetRepository<ICategoryRepository>().WithKey(id);

      this.Storage.GetRepository<ICategoryRepository>().Delete(category);
      this.Storage.Save();
      Event<ICategoryDeletedEventHandler, IRequestHandler, Category>.Broadcast(this, category);
      return this.RedirectToAction("Index");
    }
  }
}