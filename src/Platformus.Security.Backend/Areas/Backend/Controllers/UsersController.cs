// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNet.Mvc;
using Platformus.Barebone.Backend.Controllers;
using Platformus.Security.Backend.ViewModels.Users;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Backend.Controllers
{
  [Area("Backend")]
  public class UsersController : ControllerBase
  {
    public UsersController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string orderBy = "name", string direction = "asc", int skip = 0, int take = 10)
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
        User user = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IUserRepository>().Create(user);

        else this.Storage.GetRepository<IUserRepository>().Edit(user);

        this.Storage.Save();
        this.CreateOrEditUserRoles(user);
        return this.Redirect(this.Request.CombineUrl("/backend/users"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      this.Storage.GetRepository<IUserRepository>().Delete(id);
      this.Storage.Save();
      return this.RedirectToAction("Index");
    }

    private void CreateOrEditUserRoles(User user)
    {
      this.DeleteUserRoles(user);
      this.CreateUserRoles(user);
    }

    private void DeleteUserRoles(User user)
    {
      foreach (UserRole userRole in this.Storage.GetRepository<IUserRoleRepository>().FilteredByUserId(user.Id))
        this.Storage.GetRepository<IUserRoleRepository>().Delete(userRole);

      this.Storage.Save();
    }

    private void CreateUserRoles(User user)
    {
      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("role") && this.Request.Form[key] == true.ToString().ToLower())
        {
          string roleId = key.Replace("role", string.Empty);

          this.CreateUserRole(user, int.Parse(roleId));
        }
      }

      this.Storage.Save();
    }

    private void CreateUserRole(User user, int roleId)
    {
      UserRole userRole = new UserRole();

      userRole.UserId = user.Id;
      userRole.RoleId = roleId;
      this.Storage.GetRepository<IUserRoleRepository>().Create(userRole);
    }
  }
}