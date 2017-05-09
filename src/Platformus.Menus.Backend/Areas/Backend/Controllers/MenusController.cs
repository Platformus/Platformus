// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Menus.Backend.ViewModels.Menus;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Models;

namespace Platformus.Menus.Backend.Controllers
{
  [Area("Backend")]
  public class MenusController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public MenusController(IStorage storage)
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
        Menu menu = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(menu);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IMenuRepository>().Create(menu);

        else this.Storage.GetRepository<IMenuRepository>().Edit(menu);

        this.Storage.Save();
        new SerializationManager(this).SerializeMenu(menu);
        return this.RedirectToAction("Index");
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      this.Storage.GetRepository<IMenuRepository>().Delete(id);
      this.Storage.Save();
      return this.RedirectToAction("Index");
    }
  }
}