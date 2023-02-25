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
using Platformus.Core.Backend.ViewModels.Permissions;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers;

[Authorize(Policy = Policies.HasManagePermissionsPermission)]
public class PermissionsController : ControllerBase
{
  private IStringLocalizer localizer;

  private IRepository<int, Permission, PermissionFilter> Repository
  {
    get => this.Storage.GetRepository<int, Permission, PermissionFilter>();
  }

  public PermissionsController(IStorage storage, IStringLocalizer<SharedResource> localizer)
    : base(storage)
  {
    this.localizer = localizer;
  }

  public async Task<IActionResult> IndexAsync([FromQuery] PermissionFilter filter = null, string sorting = "+position", int offset = 0, int limit = 10)
  {
    return this.View(IndexViewModelFactory.Create(
      sorting, offset, limit, await this.Repository.CountAsync(filter),
      await this.Repository.GetAllAsync(filter, sorting, offset, limit)
    ));
  }

  [HttpGet]
  [ImportModelStateFromTempData]
  public async Task<IActionResult> CreateOrEditAsync(int? id)
  {
    return this.View(CreateOrEditViewModelFactory.Create(
      id == null ? null : await this.Repository.GetByIdAsync((int)id)
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
      Permission permission = CreateOrEditViewModelMapper.Map(
        createOrEdit.Id == null ? new Permission() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
        createOrEdit
      );

      if (createOrEdit.Id == null)
        this.Repository.Create(permission);

      else this.Repository.Edit(permission);

      await this.Storage.SaveAsync();

      if (createOrEdit.Id == null)
        Event<IPermissionCreatedEventHandler, HttpContext, Permission>.Broadcast(this.HttpContext, permission);

      else Event<IPermissionEditedEventHandler, HttpContext, Permission>.Broadcast(this.HttpContext, permission);

      return this.Redirect(this.Request.CombineUrl("/backend/permissions"));
    }

    return this.CreateRedirectToSelfResult();
  }

  public async Task<IActionResult> DeleteAsync(int id)
  {
    Permission permission = await this.Repository.GetByIdAsync(id);

    this.Repository.Delete(permission.Id);
    await this.Storage.SaveAsync();
    Event<IPermissionDeletedEventHandler, HttpContext, Permission>.Broadcast(this.HttpContext, permission);
    return this.Redirect(this.Request.CombineUrl("/backend/permissions"));
  }

  private async Task<bool> IsCodeUniqueAsync(CreateOrEditViewModel createOrEdit)
  {
    Permission permission = (await this.Repository.GetAllAsync(new PermissionFilter(code: createOrEdit.Code))).FirstOrDefault();

    return permission == null || permission.Id == createOrEdit.Id;
  }
}