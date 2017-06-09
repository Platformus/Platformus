// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Configurations.Backend.ViewModels.Variables;
using Platformus.Configurations.Data.Abstractions;
using Platformus.Configurations.Data.Models;

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
      if (this.ModelState.IsValid)
      {
        Variable variable = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IVariableRepository>().Create(variable);

        else this.Storage.GetRepository<IVariableRepository>().Edit(variable);

        this.Storage.Save();
        ConfigurationManager.InvalidateCache();
        return this.RedirectToAction("Index", "Configurations");
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      this.Storage.GetRepository<IVariableRepository>().Delete(id);
      this.Storage.Save();
      ConfigurationManager.InvalidateCache();
      return this.RedirectToAction("Index", "Configurations");
    }
  }
}