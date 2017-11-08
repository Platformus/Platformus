// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Configurations.Backend.ViewModels.Variables;
using Platformus.Configurations.Data.Abstractions;
using Platformus.Configurations.Data.Entities;
using Platformus.Configurations.Events;

namespace Platformus.Configurations.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseConfigurationsPermission)]
  public class VariablesController : Platformus.Barebone.Backend.Controllers.ControllerBase
  {
    public VariablesController(IStorage storage)
      : base(storage)
    {
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
      if (createOrEdit.Id == null && !this.IsCodeUnique(createOrEdit.ConfigurationId, createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Variable variable = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IVariableRepository>().Create(variable);

        else this.Storage.GetRepository<IVariableRepository>().Edit(variable);

        this.Storage.Save();

        if (createOrEdit.Id == null)
          Event<IVariableCreatedEventHandler, IRequestHandler, Variable>.Broadcast(this, variable);

        else Event<IVariableEditedEventHandler, IRequestHandler, Variable>.Broadcast(this, variable);

        return this.RedirectToAction("Index", "Configurations");
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Variable variable = this.Storage.GetRepository<IVariableRepository>().WithKey(id);

      this.Storage.GetRepository<IVariableRepository>().Delete(variable);
      this.Storage.Save();
      Event<IVariableDeletedEventHandler, IRequestHandler, Variable>.Broadcast(this, variable);
      return this.RedirectToAction("Index", "Configurations");
    }

    private bool IsCodeUnique(int configurationId, string code)
    {
      return this.Storage.GetRepository<IVariableRepository>().WithConfigurationIdAndCode(configurationId, code) == null;
    }
  }
}