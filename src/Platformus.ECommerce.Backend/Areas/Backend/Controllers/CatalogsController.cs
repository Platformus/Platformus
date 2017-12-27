// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.ECommerce.Backend.ViewModels.Catalogs;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Events;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseCatalogsPermission)]
  public class CatalogsController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public CatalogsController(IStorage storage)
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
        Catalog catalog = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(catalog);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<ICatalogRepository>().Create(catalog);

        else this.Storage.GetRepository<ICatalogRepository>().Edit(catalog);

        this.Storage.Save();

        if (createOrEdit.Id == null)
          Event<ICatalogCreatedEventHandler, IRequestHandler, Catalog>.Broadcast(this, catalog);

        else Event<ICatalogEditedEventHandler, IRequestHandler, Catalog>.Broadcast(this, catalog);

        return this.RedirectToAction("Index");
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Catalog catalog = this.Storage.GetRepository<ICatalogRepository>().WithKey(id);

      this.Storage.GetRepository<ICatalogRepository>().Delete(catalog);
      this.Storage.Save();
      Event<ICatalogDeletedEventHandler, IRequestHandler, Catalog>.Broadcast(this, catalog);
      return this.RedirectToAction("Index");
    }
  }
}