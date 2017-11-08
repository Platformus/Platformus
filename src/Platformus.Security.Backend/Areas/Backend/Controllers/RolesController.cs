// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Security.Backend.ViewModels.Roles;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;
using Platformus.Security.Events;

namespace Platformus.Security.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseRolesPermission)]
  public class RolesController : Barebone.Backend.Controllers.ControllerBase
  {
    public RolesController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string orderBy = "position", string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(orderBy, direction, skip, take, filter));
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
        Role role = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IRoleRepository>().Create(role);

        else this.Storage.GetRepository<IRoleRepository>().Edit(role);

        this.Storage.Save();
        this.CreateOrEditRolePermissions(role);

        if (createOrEdit.Id == null)
          Event<IRoleCreatedEventHandler, IRequestHandler, Role>.Broadcast(this, role);

        else Event<IRoleEditedEventHandler, IRequestHandler, Role>.Broadcast(this, role);

        return this.Redirect(this.Request.CombineUrl("/backend/roles"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Role role = this.Storage.GetRepository<IRoleRepository>().WithKey(id);

      this.Storage.GetRepository<IRoleRepository>().Delete(role);
      this.Storage.Save();
      Event<IRoleDeletedEventHandler, IRequestHandler, Role>.Broadcast(this, role);
      return this.RedirectToAction("Index");
    }

    private bool IsCodeUnique(string code)
    {
      return this.Storage.GetRepository<IRoleRepository>().WithCode(code) == null;
    }

    private void CreateOrEditRolePermissions(Role role)
    {
      this.DeleteRolePermissions(role);
      this.CreateRolePermissions(role);
    }

    private void DeleteRolePermissions(Role role)
    {
      foreach (RolePermission rolePermission in this.Storage.GetRepository<IRolePermissionRepository>().FilteredByRoleId(role.Id))
        this.Storage.GetRepository<IRolePermissionRepository>().Delete(rolePermission);

      this.Storage.Save();
    }

    private void CreateRolePermissions(Role role)
    {
      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("permission") && this.Request.Form[key] == true.ToString().ToLower())
        {
          string permissionId = key.Replace("permission", string.Empty);

          this.CreateRolePermission(role, int.Parse(permissionId));
        }
      }

      this.Storage.Save();
    }

    private void CreateRolePermission(Role role, int permissionId)
    {
      RolePermission rolePermission = new RolePermission();

      rolePermission.RoleId = role.Id;
      rolePermission.PermissionId = permissionId;
      this.Storage.GetRepository<IRolePermissionRepository>().Create(rolePermission);
    }
  }
}