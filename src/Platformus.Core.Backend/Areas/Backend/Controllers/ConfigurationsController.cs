// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Backend.ViewModels.Configurations;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageConfigurationsPermission)]
  public class ConfigurationsController : ControllerBase
  {
    private IRepository<int, Configuration, ConfigurationFilter> Repository
    {
      get => this.Storage.GetRepository<int, Configuration, ConfigurationFilter>();
    }

    public ConfigurationsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync()
    {
      return this.View(new IndexViewModelFactory().Create(
        await this.Repository.GetAllAsync(inclusions: new Inclusion<Configuration>(c => c.Variables))
      ));
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
    public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
    {
      if (createOrEdit.Id == null && !await this.IsCodeUniqueAsync(createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Configuration configuration = new CreateOrEditViewModelMapper().Map(
          createOrEdit.Id == null ? new Configuration() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(configuration);

        else this.Repository.Edit(configuration);

        await this.Storage.SaveAsync();

        if (createOrEdit.Id == null)
          Event<IConfigurationCreatedEventHandler, HttpContext, Configuration>.Broadcast(this.HttpContext, configuration);

        else Event<IConfigurationEditedEventHandler, HttpContext, Configuration>.Broadcast(this.HttpContext, configuration);

        return this.RedirectToAction("Index");
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Configuration configuration = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(configuration.Id);
      await this.Storage.SaveAsync();
      Event<IConfigurationDeletedEventHandler, HttpContext, Configuration>.Broadcast(this.HttpContext, configuration);
      return this.RedirectToAction("Index");
    }

    private async Task<bool> IsCodeUniqueAsync(string code)
    {
      return await this.Repository.CountAsync(new ConfigurationFilter() { Code = code }) == 0;
    }
  }
}