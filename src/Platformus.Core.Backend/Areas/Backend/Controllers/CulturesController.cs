// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Cultures;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Authorize(Policy = Policies.HasManageCulturesPermission)]
  public class CulturesController : ControllerBase
  {
    private IStringLocalizer localizer;

    private IRepository<string, Culture, CultureFilter> Repository
    {
      get => this.Storage.GetRepository<string, Culture, CultureFilter>();
    }

    public CulturesController(IStorage storage, IStringLocalizer<SharedResource> localizer)
      : base(storage)
    {
      this.localizer = localizer;
    }

    public async Task<IActionResult> IndexAsync([FromQuery]CultureFilter filter = null, string sorting = "+name", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(
        sorting, offset, limit, await this.Repository.CountAsync(filter),
        await this.Repository.GetAllAsync(filter, sorting, offset, limit)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(string id)
    {
      return this.View(CreateOrEditViewModelFactory.Create(
        string.IsNullOrEmpty(id) ? null : await this.Repository.GetByIdAsync(id)
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromRoute]string id, CreateOrEditViewModel createOrEdit)
    {
      if (string.IsNullOrEmpty(id) && !await this.IsIdUniqueAsync(createOrEdit.Id))
        this.ModelState.AddModelError("id", this.localizer["Value is already in use"]);

      if (this.ModelState.IsValid)
      {
        Culture culture = CreateOrEditViewModelMapper.Map(
          string.IsNullOrEmpty(id) ? new Culture() : await this.Repository.GetByIdAsync(createOrEdit.Id),
          createOrEdit
        );

        if (string.IsNullOrEmpty(id))
          this.Repository.Create(culture);

        else this.Repository.Edit(culture);

        await this.Storage.SaveAsync();

        if (string.IsNullOrEmpty(id))
          Event<ICultureCreatedEventHandler, HttpContext, Culture>.Broadcast(this.HttpContext, culture);

        else Event<ICultureEditedEventHandler, HttpContext, Culture>.Broadcast(this.HttpContext, culture);

        return this.Redirect(this.Request.CombineUrl("/backend/cultures"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(string id)
    {
      Culture culture = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(culture.Id);
      await this.Storage.SaveAsync();
      Event<ICultureDeletedEventHandler, HttpContext, Culture>.Broadcast(this.HttpContext, culture);
      return this.Redirect(this.Request.CombineUrl("/backend/cultures"));
    }

    private async Task<bool> IsIdUniqueAsync(string id)
    {
      return await this.Repository.CountAsync(new CultureFilter(id: id)) == 0;
    }
  }
}