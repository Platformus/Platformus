// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Roles;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers;

[Authorize(Policy = Policies.HasManageRolesPermission)]
public class RolesController : ControllerBase
{
  private IStringLocalizer localizer;

  private IRepository<int, Role, RoleFilter> RoleRepository
  {
    get => this.Storage.GetRepository<int, Role, RoleFilter>();
  }

  private IRepository<int, int, RolePermission, RolePermissionFilter> RolePermissionRepository
  {
    get => this.Storage.GetRepository<int, int, RolePermission, RolePermissionFilter>();
  }

  public RolesController(IStorage storage, IStringLocalizer<SharedResource> localizer)
    : base(storage)
  {
    this.localizer = localizer;
  }

  public async Task<IActionResult> IndexAsync([FromQuery] RoleFilter filter = null, string sorting = "+position", int offset = 0, int limit = 10)
  {
    return this.View(await IndexViewModelFactory.CreateAsync(
      this.HttpContext, sorting, offset, limit, await this.RoleRepository.CountAsync(filter),
      await this.RoleRepository.GetAllAsync(filter, sorting, offset, limit)
    ));
  }

  [HttpGet]
  [ImportModelStateFromTempData]
  public async Task<IActionResult> CreateOrEditAsync(int? id)
  {
    return this.View(await CreateOrEditViewModelFactory.CreateAsync(
      this.HttpContext, id == null ? null : await this.RoleRepository.GetByIdAsync((int)id, new Inclusion<Role>(r => r.RolePermissions))
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
      Role role = CreateOrEditViewModelMapper.Map(
        createOrEdit.Id == null ? new Role() : await this.RoleRepository.GetByIdAsync((int)createOrEdit.Id, new Inclusion<Role>(r => r.RolePermissions)),
        createOrEdit
      );

      if (createOrEdit.Id == null)
        this.RoleRepository.Create(role);

      else this.RoleRepository.Edit(role);

      this.MergeRolePermissions(role);
      await this.Storage.SaveAsync();

      if (createOrEdit.Id == null)
        Event<IRoleCreatedEventHandler, HttpContext, Role>.Broadcast(this.HttpContext, role);

      else Event<IRoleEditedEventHandler, HttpContext, Role>.Broadcast(this.HttpContext, role);

      return this.Redirect(this.Request.CombineUrl("/backend/roles"));
    }

    return this.CreateRedirectToSelfResult();
  }

  public async Task<IActionResult> DeleteAsync(int id)
  {
    Role role = await this.RoleRepository.GetByIdAsync(id);

    this.RoleRepository.Delete(role.Id);
    await this.Storage.SaveAsync();
    Event<IRoleDeletedEventHandler, HttpContext, Role>.Broadcast(this.HttpContext, role);
    return this.Redirect(this.Request.CombineUrl("/backend/roles"));
  }

  private async Task<bool> IsCodeUniqueAsync(CreateOrEditViewModel createOrEdit)
  {
    Role role = (await this.RoleRepository.GetAllAsync(new RoleFilter(code: createOrEdit.Code))).FirstOrDefault();

    return role == null || role.Id == createOrEdit.Id;
  }

  private void MergeRolePermissions(Role role)
  {
    List<int> permissionIds = new List<int>();

    foreach (string key in this.Request.Form.Keys)
      if (key.StartsWith("permission") && this.Request.Form[key].FirstOrDefault().ToBoolWithDefaultValue(false))
        permissionIds.Add(int.Parse(key.Replace("permission", string.Empty)));

    IEnumerable<RolePermission> currentRolePermissions = role.RolePermissions ?? Array.Empty<RolePermission>();

    foreach (RolePermission rolePermission in currentRolePermissions.Where(crp => !permissionIds.Any(id => id == crp.PermissionId)).ToList())
      this.RolePermissionRepository.Delete(rolePermission.RoleId, rolePermission.PermissionId);

    foreach (int permissionId in permissionIds.Where(id => !currentRolePermissions.Any(crp => crp.PermissionId == id)))
      this.RolePermissionRepository.Create(new RolePermission() { Role = role, PermissionId = permissionId });
  }
}