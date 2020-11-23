// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Backend.ViewModels.FieldOptions;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageFormsPermission)]
  public class FieldOptionsController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, FieldOption, FieldOptionFilter> Repository
    {
      get => this.Storage.GetRepository<int, FieldOption, FieldOptionFilter>();
    }

    public FieldOptionsController(IStorage storage)
      : base(storage)
    {
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(new CreateOrEditViewModelFactory().Create(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync(
          (int)id,
          new Inclusion<FieldOption>(fo => fo.Value.Localizations)
        )
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]FieldOptionFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        FieldOption fieldOption = new CreateOrEditViewModelMapper().Map(
          filter,
          createOrEdit.Id == null ? new FieldOption() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        await this.CreateOrEditEntityLocalizationsAsync(fieldOption);

        if (createOrEdit.Id == null)
          this.Repository.Create(fieldOption);

        else this.Repository.Edit(fieldOption);

        await this.Storage.SaveAsync();
        Event<IFormEditedEventHandler, HttpContext, Form>.Broadcast(this.HttpContext, await this.GetFormAsync(fieldOption));
        return this.RedirectToAction("Index", "Forms");
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      FieldOption fieldOption = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(fieldOption.Id);
      await this.Storage.SaveAsync();
      Event<IFormEditedEventHandler, HttpContext, Form>.Broadcast(this.HttpContext, await this.GetFormAsync(fieldOption));
      return this.RedirectToAction("Index", "Forms");
    }

    private async Task<Form> GetFormAsync(FieldOption fieldOption)
    {
      Field field = await this.Storage.GetRepository<int, Field, FieldFilter>().GetByIdAsync(fieldOption.FieldId, new Inclusion<Field>(f => f.Form));

      return field.Form;
    }
  }
}