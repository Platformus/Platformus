// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Backend.ViewModels.Users;
using Platformus.Core.Data.Abstractions;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageUsersPermission)]
  public class UsersController : ControllerBase
  {
    private IRepository<int, User, UserFilter> Repository
    {
      get => this.Storage.GetRepository<int, User, UserFilter>();
    }

    public UsersController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]UserFilter filter = null, string orderBy = "+name", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelFactory().Create(
        this.HttpContext, filter,
        await this.Repository.GetAllAsync(filter, orderBy, skip, take),
        orderBy, skip, take, await this.Repository.CountAsync(filter)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(await new CreateOrEditViewModelFactory().CreateAsync(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync((int)id, new Inclusion<User>(u => u.UserRoles))
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        User user = new CreateOrEditViewModelMapper().Map(
          createOrEdit.Id == null ? new User() : await this.Repository.GetByIdAsync((int)createOrEdit.Id, new Inclusion<User>(u => u.UserRoles)),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(user);

        else this.Repository.Edit(user);

        await this.Storage.SaveAsync();
        await this.CreateOrEditUserRolesAsync(user);

        if (createOrEdit.Id == null)
          Event<IUserCreatedEventHandler, HttpContext, User>.Broadcast(this.HttpContext, user);

        else Event<IUserEditedEventHandler, HttpContext, User>.Broadcast(this.HttpContext, user);

        return this.Redirect(this.Request.CombineUrl("/backend/users"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      User user = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(user.Id);
      await this.Storage.SaveAsync();
      Event<IUserDeletedEventHandler, HttpContext, User>.Broadcast(this.HttpContext, user);
      return this.RedirectToAction("Index");
    }

    private async Task CreateOrEditUserRolesAsync(User user)
    {
      await this.DeleteUserRolesAsync(user);
      await this.CreateUserRolesAsync(user);
    }

    private async Task DeleteUserRolesAsync(User user)
    {
      if (user.UserRoles != null)
        foreach (UserRole userRole in user.UserRoles)
          this.Storage.GetRepository<IUserRoleRepository>().Delete(userRole);

      await this.Storage.SaveAsync();
    }

    private async Task CreateUserRolesAsync(User user)
    {
      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("role") && this.Request.Form[key] == true.ToString().ToLower())
        {
          string roleId = key.Replace("role", string.Empty);

          this.CreateUserRole(user, int.Parse(roleId));
        }
      }

      await this.Storage.SaveAsync();
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