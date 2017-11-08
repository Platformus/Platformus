// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Configurations.Backend.ViewModels.Configurations;
using Platformus.Configurations.Data.Abstractions;
using Platformus.Configurations.Data.Entities;
using Platformus.Configurations.Events;

namespace Platformus.Configurations.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseConfigurationsPermission)]
  public class ConfigurationsController : Platformus.Barebone.Backend.Controllers.ControllerBase
  {
    public ConfigurationsController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index()
    {
      return this.View(new IndexViewModelFactory(this).Create());
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
      if (createOrEdit.Id == null && !this.IsCodeUnique(createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Configuration configuration = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IConfigurationRepository>().Create(configuration);

        else this.Storage.GetRepository<IConfigurationRepository>().Edit(configuration);

        this.Storage.Save();

        if (createOrEdit.Id == null)
          Event<IConfigurationCreatedEventHandler, IRequestHandler, Configuration>.Broadcast(this, configuration);

        else Event<IConfigurationEditedEventHandler, IRequestHandler, Configuration>.Broadcast(this, configuration);

        return this.RedirectToAction("Index");
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Configuration configuration = this.Storage.GetRepository<IConfigurationRepository>().WithKey(id);

      this.Storage.GetRepository<IConfigurationRepository>().Delete(configuration);
      this.Storage.Save();
      Event<IConfigurationDeletedEventHandler, IRequestHandler, Configuration>.Broadcast(this, configuration);
      return this.RedirectToAction("Index");
    }

    private bool IsCodeUnique(string code)
    {
      return this.Storage.GetRepository<IConfigurationRepository>().WithCode(code) == null;
    }
  }
}