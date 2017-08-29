// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Security.Backend.ViewModels.Users;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;
using Platformus.Security.Events;

namespace Platformus.Security.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseUsersPermission)]
  public class UsersController : Barebone.Backend.Controllers.ControllerBase
  {
    public UsersController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string orderBy = "name", string direction = "asc", int skip = 0, int take = 10, string filter = null)
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
      if (this.ModelState.IsValid)
      {
        User user = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IUserRepository>().Create(user);

        else this.Storage.GetRepository<IUserRepository>().Edit(user);

        this.Storage.Save();
        this.CreateOrEditUserRoles(user);

        if (createOrEdit.Id == null)
          Event<IUserCreatedEventHandler, IRequestHandler, User>.Broadcast(this, user);

        else Event<IUserEditedEventHandler, IRequestHandler, User>.Broadcast(this, user);

        return this.Redirect(this.Request.CombineUrl("/backend/users"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      User user = this.Storage.GetRepository<IUserRepository>().WithKey(id);

      this.Storage.GetRepository<IUserRepository>().Delete(user);
      this.Storage.Save();
      Event<IUserDeletedEventHandler, IRequestHandler, User>.Broadcast(this, user);
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