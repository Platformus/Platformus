// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Backend.ViewModels.Cultures;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageCulturesPermission)]
  public class CulturesController : ControllerBase
  {
    private IRepository<int, Culture, CultureFilter> Repository
    {
      get => this.Storage.GetRepository<int, Culture, CultureFilter>();
    }

    public CulturesController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]CultureFilter filter = null, string orderBy = "+name", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelFactory().Create(
        this.HttpContext, filter,
        await this.Repository.GetAllAsync(filter, orderBy, skip, take),
        orderBy, skip, take, await this.Repository.CountAsync(filter)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(new CreateOrEditViewModelFactory().Create(
        id  == null ? null : await this.Repository.GetByIdAsync((int)id)
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
        Culture culture = new CreateOrEditViewModelMapper().Map(
          createOrEdit.Id == null ? new Culture() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(culture);

        else this.Repository.Edit(culture);

        await this.Storage.SaveAsync();

        if (createOrEdit.Id == null)
          Event<ICultureCreatedEventHandler, HttpContext, Culture>.Broadcast(this.HttpContext, culture);

        else Event<ICultureEditedEventHandler, HttpContext, Culture>.Broadcast(this.HttpContext, culture);

        return this.Redirect(this.Request.CombineUrl("/backend/cultures"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Culture culture = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(culture.Id);
      await this.Storage.SaveAsync();
      Event<ICultureDeletedEventHandler, HttpContext, Culture>.Broadcast(this.HttpContext, culture);
      return this.RedirectToAction("Index");
    }

    private async Task<bool> IsCodeUniqueAsync(string code)
    {
      return await this.Repository.CountAsync(new CultureFilter() { Code = code }) == 0;
    }
  }
}