// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Backend.ViewModels.Permissions;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManagePermissionsPermission)]
  public class PermissionsController : ControllerBase
  {
    private IRepository<int, Permission, PermissionFilter> Repository
    {
      get => this.Storage.GetRepository<int, Permission, PermissionFilter>();
    }

    public PermissionsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]PermissionFilter filter = null, string orderBy = "+position", int skip = 0, int take = 10)
    {
      return this.View(IndexViewModelFactory.Create(
        this.HttpContext, filter,
        await this.Repository.GetAllAsync(filter, orderBy, skip, take),
        orderBy, skip, take, await this.Repository.CountAsync(filter)
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
      if (createOrEdit.Id == null && !await this.IsCodeUniqueAsync(createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

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

    private async Task<bool> IsCodeUniqueAsync(string code)
    {
      return await this.Repository.CountAsync(new PermissionFilter(code: code)) == 0;
    }
  }
}