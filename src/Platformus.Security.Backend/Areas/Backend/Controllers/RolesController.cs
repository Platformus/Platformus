// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNet.Mvc;
using Platformus.Barebone.Backend.Controllers;
using Platformus.Security.Backend.ViewModels.Roles;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Backend.Controllers
{
  [Area("Backend")]
  public class RolesController : ControllerBase
  {
    public RolesController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string orderBy = "position", string direction = "asc", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelBuilder(this).Build(orderBy, direction, skip, take));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public IActionResult CreateOrEdit(int? id)
    {
      return this.View(new CreateOrEditViewModelBuilder(this).Build(id));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public IActionResult CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Role role = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IRoleRepository>().Create(role);

        else this.Storage.GetRepository<IRoleRepository>().Edit(role);

        this.Storage.Save();
        this.CreateOrEditRolePermissions(role);
        return this.Redirect(this.Request.CombineUrl("/backend/roles"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      this.Storage.GetRepository<IRoleRepository>().Delete(id);
      this.Storage.Save();
      return this.RedirectToAction("Index");
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