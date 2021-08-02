// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Backend.ViewModels.Classes;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageClassesPermission)]
  public class ClassesController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, Class, ClassFilter> Repository
    {
      get => this.Storage.GetRepository<int, Class, ClassFilter>();
    }

    public ClassesController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]ClassFilter filter = null, string orderBy = "+name", int skip = 0, int take = 10)
    {
      return this.View(await IndexViewModelFactory.CreateAsync(
        this.HttpContext, filter,
        await this.Repository.GetAllAsync(filter, orderBy, skip, take, new Inclusion<Class>(c => c.Parent)),
        orderBy, skip, take, await this.Repository.CountAsync(filter)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(await CreateOrEditViewModelFactory.CreateAsync(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync((int)id)
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
        Class @class = CreateOrEditViewModelMapper.Map(
          createOrEdit.Id == null ? new Class() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(@class);

        else this.Repository.Edit(@class);

        await this.Storage.SaveAsync();

        if (createOrEdit.Id == null)
          Event<IClassCreatedEventHandler, HttpContext, Class>.Broadcast(this.HttpContext, @class);

        else Event<IClassEditedEventHandler, HttpContext, Class>.Broadcast(this.HttpContext, @class);

        return this.Redirect(this.Request.CombineUrl("/backend/classes"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Class @class = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(@class.Id);
      await this.Storage.SaveAsync();
      Event<IClassDeletedEventHandler, HttpContext, Class>.Broadcast(this.HttpContext, @class);
      return this.Redirect(this.Request.CombineUrl("/backend/classes"));
    }

    private async Task<bool> IsCodeUniqueAsync(string code)
    {
      return await this.Repository.CountAsync(new ClassFilter(code: code)) == 0;
    }
  }
}