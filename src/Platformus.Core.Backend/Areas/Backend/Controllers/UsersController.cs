// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Backend.ViewModels.Users;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageUsersPermission)]
  public class UsersController : ControllerBase
  {
    private IRepository<int, User, UserFilter> UserRepository
    {
      get => this.Storage.GetRepository<int, User, UserFilter>();
    }

    private IRepository<int, int, UserRole, UserRoleFilter> UserRoleRepository
    {
      get => this.Storage.GetRepository<int, int, UserRole, UserRoleFilter>();
    }

    public UsersController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]UserFilter filter = null, string sorting = "+name", int offset = 0, int limit = 10)
    {
      return this.View(await IndexViewModelFactory.CreateAsync(
        this.HttpContext, sorting, offset, limit, await this.UserRepository.CountAsync(filter),
        await this.UserRepository.GetAllAsync(filter, sorting, offset, limit)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(await CreateOrEditViewModelFactory.CreateAsync(
        this.HttpContext, id == null ? null : await this.UserRepository.GetByIdAsync((int)id, new Inclusion<User>(u => u.UserRoles))
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        User user = CreateOrEditViewModelMapper.Map(
          createOrEdit.Id == null ? new User() : await this.UserRepository.GetByIdAsync((int)createOrEdit.Id, new Inclusion<User>(u => u.UserRoles)),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.UserRepository.Create(user);

        else this.UserRepository.Edit(user);

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
      User user = await this.UserRepository.GetByIdAsync(id);

      this.UserRepository.Delete(user.Id);
      await this.Storage.SaveAsync();
      Event<IUserDeletedEventHandler, HttpContext, User>.Broadcast(this.HttpContext, user);
      return this.Redirect(this.Request.CombineUrl("/backend/users"));
    }

    private async Task CreateOrEditUserRolesAsync(User user)
    {
      await this.DeleteUserRolesAsync(user);
      await this.CreateUserRolesAsync(user);
    }

    private async Task DeleteUserRolesAsync(User user)
    {
      if (user.UserRoles != null)
        for (int i = 0; i != user.UserRoles.Count; i++)
        {
          UserRole userRole = user.UserRoles.ElementAt(i);

          this.UserRoleRepository.Delete(userRole.UserId, userRole.RoleId);
        }

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
      this.UserRoleRepository.Create(userRole);
    }
  }
}