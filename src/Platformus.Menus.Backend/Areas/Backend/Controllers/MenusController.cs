// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Menus.Backend.ViewModels.Menus;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Entities;
using Platformus.Menus.Events;

namespace Platformus.Menus.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseMenusPermission)]
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
      if (createOrEdit.Id == null && !this.IsCodeUnique(createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Menu menu = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(menu);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IMenuRepository>().Create(menu);

        else this.Storage.GetRepository<IMenuRepository>().Edit(menu);

        this.Storage.Save();

        if (createOrEdit.Id == null)
          Event<IMenuCreatedEventHandler, IRequestHandler, Menu>.Broadcast(this, menu);

        else Event<IMenuEditedEventHandler, IRequestHandler, Menu>.Broadcast(this, menu);

        return this.RedirectToAction("Index");
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Menu menu = this.Storage.GetRepository<IMenuRepository>().WithKey(id);

      this.Storage.GetRepository<IMenuRepository>().Delete(menu);
      this.Storage.Save();
      Event<IMenuDeletedEventHandler, IRequestHandler, Menu>.Broadcast(this, menu);
      return this.RedirectToAction("Index");
    }

    private bool IsCodeUnique(string code)
    {
      return this.Storage.GetRepository<IMenuRepository>().WithCode(code) == null;
    }
  }
}