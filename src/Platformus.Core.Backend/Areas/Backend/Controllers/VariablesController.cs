// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Backend.ViewModels.Variables;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageConfigurationsPermission)]
  public class VariablesController : ControllerBase
  {
    private IRepository<int, Variable, VariableFilter> Repository
    {
      get => this.Storage.GetRepository<int, Variable, VariableFilter>();
    }

    public VariablesController(IStorage storage)
      : base(storage)
    {
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(new CreateOrEditViewModelFactory().Create(
        id == null ? null : await this.Repository.GetByIdAsync((int)id)
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]VariableFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (createOrEdit.Id == null && !await this.IsCodeUniqueAsync(filter, createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Variable variable = new CreateOrEditViewModelMapper().Map(
          filter,
          createOrEdit.Id == null ? new Variable() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(variable);

        else this.Repository.Edit(variable);

        await this.Storage.SaveAsync();

        if (createOrEdit.Id == null)
          Event<IVariableCreatedEventHandler, HttpContext, Variable>.Broadcast(this.HttpContext, variable);

        else Event<IVariableEditedEventHandler, HttpContext, Variable>.Broadcast(this.HttpContext, variable);

        return this.RedirectToAction("Index", "Configurations");
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Variable variable = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(variable.Id);
      await this.Storage.SaveAsync();
      Event<IVariableDeletedEventHandler, HttpContext, Variable>.Broadcast(this.HttpContext, variable);
      return this.RedirectToAction("Index", "Configurations");
    }

    private async Task<bool> IsCodeUniqueAsync(VariableFilter filter, string code)
    {
      filter.Code = code;
      return await this.Repository.CountAsync(filter) == 0;
    }
  }
}