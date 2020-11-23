// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Backend.ViewModels.MenuItems;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageMenusPermission)]
  public class MenuItemsController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, MenuItem, MenuItemFilter> Repository
    {
      get => this.Storage.GetRepository<int, MenuItem, MenuItemFilter>();
    }

    public MenuItemsController(IStorage storage)
      : base(storage)
    {
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(new CreateOrEditViewModelFactory().Create(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync(
          (int)id,
          new Inclusion<MenuItem>(mi => mi.Name.Localizations)
        )
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]MenuItemFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        MenuItem menuItem = new CreateOrEditViewModelMapper().Map(
          filter,
          createOrEdit.Id == null ? new MenuItem() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        await this.CreateOrEditEntityLocalizationsAsync(menuItem);

        if (createOrEdit.Id == null)
          this.Repository.Create(menuItem);

        else this.Repository.Edit(menuItem);

        await this.Storage.SaveAsync();
        Event<IMenuEditedEventHandler, HttpContext, Menu>.Broadcast(this.HttpContext, await this.GetMenuAsync(menuItem));
        return this.RedirectToAction("Index", "Menus");
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      MenuItem menuItem = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(menuItem.Id);
      await this.Storage.SaveAsync();
      Event<IMenuEditedEventHandler, HttpContext, Menu>.Broadcast(this.HttpContext, await this.GetMenuAsync(menuItem));
      return this.RedirectToAction("Index", "Menus");
    }

    private async Task<Menu> GetMenuAsync(MenuItem menuItem)
    {
      while (menuItem.MenuId == null)
        menuItem = await this.Repository.GetByIdAsync((int)menuItem.MenuItemId);

      return await this.Storage.GetRepository<int, Menu, MenuFilter>().GetByIdAsync((int)menuItem.MenuId);
    }
  }
}