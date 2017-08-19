// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Menus.Backend.ViewModels.MenuItems;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Entities;
using Platformus.Menus.Events;

namespace Platformus.Menus.Backend
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseMenusPermission)]
  public class MenuItemsController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public MenuItemsController(IStorage storage)
      : base(storage)
    {
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
        MenuItem menuItem = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(menuItem);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IMenuItemRepository>().Create(menuItem);

        else this.Storage.GetRepository<IMenuItemRepository>().Edit(menuItem);

        this.Storage.Save();
        Event<IMenuEditedEventHandler, IRequestHandler, Menu>.Broadcast(this, this.GetMenu(menuItem));
        return this.RedirectToAction("Index", "Menus");
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      MenuItem menuItem = this.Storage.GetRepository<IMenuItemRepository>().WithKey(id);
      Menu menu = this.GetMenu(menuItem);

      this.Storage.GetRepository<IMenuItemRepository>().Delete(menuItem);
      this.Storage.Save();
      new SerializationManager(this).SerializeMenu(menu);
      return this.RedirectToAction("Index", "Menus");
    }

    private Menu GetMenu(MenuItem menuItem)
    {
      while (menuItem.MenuId == null)
        menuItem = this.Storage.GetRepository<IMenuItemRepository>().WithKey((int)menuItem.MenuItemId);

      return this.Storage.GetRepository<IMenuRepository>().WithKey((int)menuItem.MenuId);
    }
  }
}