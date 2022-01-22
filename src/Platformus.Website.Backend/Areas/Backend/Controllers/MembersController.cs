// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend;
using Platformus.Website.Backend.ViewModels.Members;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Authorize(Policy = Policies.HasManageClassesPermission)]
  public class MembersController : Core.Backend.Controllers.ControllerBase
  {
    private IStringLocalizer localizer;

    private IRepository<int, Member, MemberFilter> Repository
    {
      get => this.Storage.GetRepository<int, Member, MemberFilter>();
    }

    public MembersController(IStorage storage, IStringLocalizer<SharedResource> localizer)
      : base(storage)
    {
      this.localizer = localizer;
    }

    public async Task<IActionResult> IndexAsync([FromQuery]MemberFilter filter = null, string sorting = "+position", int offset = 0, int limit = 10)
    {
      return this.View(await IndexViewModelFactory.CreateAsync(
        this.HttpContext, filter, sorting, offset, limit, await this.Repository.CountAsync(filter),
        await this.Repository.GetAllAsync(
          filter, sorting, offset, limit,
          new Inclusion<Member>(m => m.PropertyDataType),
          new Inclusion<Member>(m => m.RelationClass)
        )
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]MemberFilter filter, int? id)
    {
      return this.View(await CreateOrEditViewModelFactory.CreateAsync(
        this.HttpContext, filter, id == null ? null : await this.Repository.GetByIdAsync((int)id)
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]MemberFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (!await this.IsCodeUniqueAsync(filter, createOrEdit))
        this.ModelState.AddModelError("code", this.localizer["Value is already in use"]);

      if (this.ModelState.IsValid)
      {
        Member member = CreateOrEditViewModelMapper.Map(
          filter,
          createOrEdit.Id == null ? new Member() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(member);

        else this.Repository.Edit(member);

        await this.Storage.SaveAsync();

        if (createOrEdit.Id == null)
          Event<IMemberCreatedEventHandler, HttpContext, Member>.Broadcast(this.HttpContext, member);

        else Event<IMemberEditedEventHandler, HttpContext, Member>.Broadcast(this.HttpContext, member);

        return this.Redirect(this.Request.CombineUrl("/backend/members"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Member member = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(member.Id);
      await this.Storage.SaveAsync();
      Event<IMemberDeletedEventHandler, HttpContext, Member>.Broadcast(this.HttpContext, member);
      return this.Redirect(this.Request.CombineUrl("/backend/members"));
    }

    private async Task<bool> IsCodeUniqueAsync(MemberFilter filter, CreateOrEditViewModel createOrEdit)
    {
      filter.Code = createOrEdit.Code;

      Member member = (await this.Repository.GetAllAsync(filter)).FirstOrDefault();

      return member == null || member.Id == createOrEdit.Id;
    }
  }
}