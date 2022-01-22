// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Users;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Authorize(Policy = Policies.HasManageUsersPermission)]
  public class UsersController : ControllerBase
  {
    private IStringLocalizer localizer;

    private IRepository<int, User, UserFilter> UserRepository
    {
      get => this.Storage.GetRepository<int, User, UserFilter>();
    }

    private IRepository<int, int, UserRole, UserRoleFilter> UserRoleRepository
    {
      get => this.Storage.GetRepository<int, int, UserRole, UserRoleFilter>();
    }

    public UsersController(IStorage storage, IStringLocalizer<SharedResource> localizer)
      : base(storage)
    {
      this.localizer = localizer;
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
      if (!await this.IsNameUniqueAsync(createOrEdit))
        this.ModelState.AddModelError("name", this.localizer["Value is already in use"]);

      if (this.ModelState.IsValid)
      {
        User user = CreateOrEditViewModelMapper.Map(
          createOrEdit.Id == null ? new User() : await this.UserRepository.GetByIdAsync((int)createOrEdit.Id, new Inclusion<User>(u => u.UserRoles)),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.UserRepository.Create(user);

        else this.UserRepository.Edit(user);

        this.MergeUserRoles(user);
        await this.Storage.SaveAsync();

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

    private async Task<bool> IsNameUniqueAsync(CreateOrEditViewModel createOrEdit)
    {
      User user = (await this.UserRepository.GetAllAsync(new UserFilter(name: new StringFilter(equals: createOrEdit.Name)))).FirstOrDefault();

      return user == null || user.Id == createOrEdit.Id;
    }

    private void MergeUserRoles(User user)
    {
      List<int> roleIds = new List<int>();

      foreach (string key in this.Request.Form.Keys)
        if (key.StartsWith("role") && this.Request.Form[key].FirstOrDefault().ToBoolWithDefaultValue(false))
          roleIds.Add(int.Parse(key.Replace("role", string.Empty)));

      IEnumerable<UserRole> currentUserRoles = user.UserRoles ?? Array.Empty<UserRole>();

      foreach (UserRole userRole in currentUserRoles.Where(cur => !roleIds.Any(id => id == cur.RoleId)).ToList())
        this.UserRoleRepository.Delete(userRole.UserId, userRole.RoleId);

      foreach (int roleId in roleIds.Where(id => !currentUserRoles.Any(cur => cur.RoleId == id)))
        this.UserRoleRepository.Create(new UserRole() { User = user, RoleId = roleId });
    }
  }
}