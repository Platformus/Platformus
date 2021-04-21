// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Backend.ViewModels.Roles;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageRolesPermission)]
  public class RolesController : ControllerBase
  {
    private IRepository<int, Role, RoleFilter> RoleRepository
    {
      get => this.Storage.GetRepository<int, Role, RoleFilter>();
    }

    private IRepository<int, int, RolePermission, RolePermissionFilter> RolePermissionRepository
    {
      get => this.Storage.GetRepository<int, int, RolePermission, RolePermissionFilter>();
    }

    public RolesController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]RoleFilter filter = null, string orderBy = "+position", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelFactory().Create(
        this.HttpContext, filter,
        await this.RoleRepository.GetAllAsync(filter, orderBy, skip, take),
        orderBy, skip, take, await this.RoleRepository.CountAsync(filter)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(await new CreateOrEditViewModelFactory().CreateAsync(
        this.HttpContext, id == null ? null : await this.RoleRepository.GetByIdAsync((int)id, new Inclusion<Role>(r => r.RolePermissions))
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
        Role role = new CreateOrEditViewModelMapper().Map(
          createOrEdit.Id == null ? new Role() : await this.RoleRepository.GetByIdAsync((int)createOrEdit.Id, new Inclusion<Role>(r => r.RolePermissions)),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.RoleRepository.Create(role);

        else this.RoleRepository.Edit(role);

        await this.Storage.SaveAsync();
        await this.CreateOrEditRolePermissionsAsync(role);

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
      return this.RedirectToAction("Index");
    }

    private async Task<bool> IsCodeUniqueAsync(string code)
    {
      return await this.RoleRepository.CountAsync(new RoleFilter(code: code)) == 0;
    }

    private async Task CreateOrEditRolePermissionsAsync(Role role)
    {
      await this.DeleteRolePermissionsAsync(role);
      await this.CreateRolePermissionsAsync(role);
    }

    private async Task DeleteRolePermissionsAsync(Role role)
    {
      if (role.RolePermissions != null)
        for (int i = 0; i != role.RolePermissions.Count; i++)
        {
          RolePermission rolePermission = role.RolePermissions.ToArray()[i];

          this.RolePermissionRepository.Delete(rolePermission.RoleId, rolePermission.PermissionId);
        }

      await this.Storage.SaveAsync();
    }

    private async Task CreateRolePermissionsAsync(Role role)
    {
      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("permission") && this.Request.Form[key] == true.ToString().ToLower())
        {
          string permissionId = key.Replace("permission", string.Empty);

          this.CreateRolePermission(role, int.Parse(permissionId));
        }
      }

      await this.Storage.SaveAsync();
    }

    private void CreateRolePermission(Role role, int permissionId)
    {
      RolePermission rolePermission = new RolePermission();

      rolePermission.RoleId = role.Id;
      rolePermission.PermissionId = permissionId;
      this.RolePermissionRepository.Create(rolePermission);
    }
  }
}