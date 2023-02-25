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
using Platformus.Core.Backend.ViewModels.Configurations;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers;

[Authorize(Policy = Policies.HasManageConfigurationsPermission)]
public class ConfigurationsController : ControllerBase
{
  private IStringLocalizer localizer;

  private IRepository<int, Configuration, ConfigurationFilter> Repository
  {
    get => this.Storage.GetRepository<int, Configuration, ConfigurationFilter>();
  }

  public ConfigurationsController(IStorage storage, IStringLocalizer<SharedResource> localizer)
    : base(storage)
  {
    this.localizer = localizer;
  }

  public async Task<IActionResult> IndexAsync()
  {
    return this.View(IndexViewModelFactory.Create(
      await this.Repository.GetAllAsync(inclusions: new Inclusion<Configuration>(c => c.Variables))
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
  public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
  {
    if (!await this.IsCodeUniqueAsync(createOrEdit))
      this.ModelState.AddModelError("code", this.localizer["Value is already in use"]);

    if (this.ModelState.IsValid)
    {
      Configuration configuration = CreateOrEditViewModelMapper.Map(
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

  private async Task<bool> IsCodeUniqueAsync(CreateOrEditViewModel createOrEdit)
  {
    Configuration configuration = (await this.Repository.GetAllAsync(new ConfigurationFilter(code: createOrEdit.Code))).FirstOrDefault();

    return configuration == null || configuration.Id == createOrEdit.Id;
  }
}