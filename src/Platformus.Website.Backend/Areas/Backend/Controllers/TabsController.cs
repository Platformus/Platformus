// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Backend.ViewModels.Tabs;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageClassesPermission)]
  public class TabsController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, Tab, TabFilter> Repository
    {
      get => this.Storage.GetRepository<int, Tab, TabFilter>();
    }

    public TabsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]TabFilter filter = null, string orderBy = "+position", int skip = 0, int take = 10)
    {
      return this.View(IndexViewModelFactory.Create(
        this.HttpContext, filter,
        await this.Repository.GetAllAsync(filter, orderBy, skip, take),
        orderBy, skip, take, await this.Repository.CountAsync(filter)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(CreateOrEditViewModelFactory.Create(
        id == null ? null : await this.Repository.GetByIdAsync((int)id)
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]TabFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Tab tab = CreateOrEditViewModelMapper.Map(
          filter,
          createOrEdit.Id == null ? new Tab() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(tab);

        else this.Repository.Edit(tab);

        await this.Storage.SaveAsync();
        return this.Redirect(this.Request.CombineUrl("/backend/tabs"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Tab tab = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(tab.Id);
      await this.Storage.SaveAsync();
      return this.Redirect(this.Request.CombineUrl("/backend/tabs"));
    }
  }
}