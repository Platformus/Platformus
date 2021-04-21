// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Backend.ViewModels.Menus;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageMenusPermission)]
  public class MenusController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, Menu, MenuFilter> Repository
    {
      get => this.Storage.GetRepository<int, Menu, MenuFilter>();
    }

    public MenusController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync()
    {
      return this.View(new IndexViewModelFactory().Create(
        await this.Repository.GetAllAsync(
          inclusions: new Inclusion<Menu>[] {
            new Inclusion<Menu>(m => m.Name.Localizations),
            new Inclusion<Menu>("MenuItems.Name.Localizations"),
            new Inclusion<Menu>("MenuItems.MenuItems.Name.Localizations"),
            new Inclusion<Menu>("MenuItems.MenuItems.MenuItems.Name.Localizations")
          }
        )
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(new CreateOrEditViewModelFactory().Create(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync(
          (int)id,
          new Inclusion<Menu>(m => m.Name.Localizations)
        )
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
    {
      if (createOrEdit.Id == null && !await this.IsCodeUniqueAsync(createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Menu menu = new CreateOrEditViewModelMapper().Map(
          createOrEdit.Id == null ? new Menu() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        await this.CreateOrEditEntityLocalizationsAsync(menu);

        if (createOrEdit.Id == null)
          this.Repository.Create(menu);

        else this.Repository.Edit(menu);

        await this.Storage.SaveAsync();

        if (createOrEdit.Id == null)
          Event<IMenuCreatedEventHandler, HttpContext, Menu>.Broadcast(this.HttpContext, menu);

        else Event<IMenuEditedEventHandler, HttpContext, Menu>.Broadcast(this.HttpContext, menu);

        return this.RedirectToAction("Index");
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Menu menu = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(menu.Id);
      await this.Storage.SaveAsync();
      Event<IMenuDeletedEventHandler, HttpContext, Menu>.Broadcast(this.HttpContext, menu);
      return this.RedirectToAction("Index");
    }

    private async Task<bool> IsCodeUniqueAsync(string code)
    {
      return await this.Repository.CountAsync(new MenuFilter(code: code)) == 0;
    }
  }
}