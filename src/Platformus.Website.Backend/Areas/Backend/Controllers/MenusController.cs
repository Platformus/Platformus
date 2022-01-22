// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend;
using Platformus.Website.Backend.ViewModels.Menus;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Authorize(Policy = Policies.HasManageMenusPermission)]
  public class MenusController : Core.Backend.Controllers.ControllerBase
  {
    private IStringLocalizer localizer;

    private IRepository<int, Menu, MenuFilter> Repository
    {
      get => this.Storage.GetRepository<int, Menu, MenuFilter>();
    }

    public MenusController(IStorage storage, IStringLocalizer<SharedResource> localizer)
      : base(storage)
    {
      this.localizer = localizer;
    }

    public async Task<IActionResult> IndexAsync()
    {
      return this.View(IndexViewModelFactory.Create(
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
      return this.View(CreateOrEditViewModelFactory.Create(
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
      if (!await this.IsCodeUniqueAsync(createOrEdit))
        this.ModelState.AddModelError("code", this.localizer["Value is already in use"]);

      if (this.ModelState.IsValid)
      {
        Menu menu = CreateOrEditViewModelMapper.Map(
          createOrEdit.Id == null ?
            new Menu() :
            await this.Repository.GetByIdAsync(
              (int)createOrEdit.Id,
              new Inclusion<Menu>(m => m.Name.Localizations)
            ),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(menu);

        else this.Repository.Edit(menu);

        await this.MergeEntityLocalizationsAsync(menu);
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

    private async Task<bool> IsCodeUniqueAsync(CreateOrEditViewModel createOrEdit)
    {
      Menu menu = (await this.Repository.GetAllAsync(new MenuFilter(code: createOrEdit.Code))).FirstOrDefault();

      return menu == null || menu.Id == createOrEdit.Id;
    }
  }
}