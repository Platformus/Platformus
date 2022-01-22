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
using Platformus.Core.Backend.ViewModels.Variables;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Authorize(Policy = Policies.HasManageConfigurationsPermission)]
  public class VariablesController : ControllerBase
  {
    private IStringLocalizer localizer;

    private IRepository<int, Variable, VariableFilter> Repository
    {
      get => this.Storage.GetRepository<int, Variable, VariableFilter>();
    }

    public VariablesController(IStorage storage, IStringLocalizer<SharedResource> localizer)
      : base(storage)
    {
      this.localizer = localizer;
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
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]VariableFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (!await this.IsCodeUniqueAsync(filter, createOrEdit))
        this.ModelState.AddModelError("code", this.localizer["Value is already in use"]);

      if (this.ModelState.IsValid)
      {
        Variable variable = CreateOrEditViewModelMapper.Map(
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

    private async Task<bool> IsCodeUniqueAsync(VariableFilter filter, CreateOrEditViewModel createOrEdit)
    {
      filter.Code = createOrEdit.Code;

      Variable variable = (await this.Repository.GetAllAsync(filter)).FirstOrDefault();

      return variable == null || variable.Id == createOrEdit.Id;
    }
  }
}