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
using Platformus.Core.Backend;
using Platformus.Website.Backend.ViewModels.Fields;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers;

[Authorize(Policy = Policies.HasManageFormsPermission)]
public class FieldsController : Core.Backend.Controllers.ControllerBase
{
  private IStringLocalizer localizer;

  private IRepository<int, Field, FieldFilter> Repository
  {
    get => this.Storage.GetRepository<int, Field, FieldFilter>();
  }

  public FieldsController(IStorage storage, IStringLocalizer<SharedResource> localizer)
    : base(storage)
  {
    this.localizer = localizer;
  }

  [HttpGet]
  [ImportModelStateFromTempData]
  public async Task<IActionResult> CreateOrEditAsync(int? id)
  {
    return this.View(await CreateOrEditViewModelFactory.CreateAsync(
      this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync(
        (int)id,
        new Inclusion<Field>(fo => fo.Name.Localizations)
      )
    ));
  }

  [HttpPost]
  [ExportModelStateToTempData]
  public async Task<IActionResult> CreateOrEditAsync([FromQuery] FieldFilter filter, CreateOrEditViewModel createOrEdit)
  {
    if (!await this.IsCodeUniqueAsync(filter, createOrEdit))
      this.ModelState.AddModelError("code", this.localizer["Value is already in use"]);

    if (this.ModelState.IsValid)
    {
      Field field = CreateOrEditViewModelMapper.Map(
        filter,
        createOrEdit.Id == null ?
          new Field() :
          await this.Repository.GetByIdAsync(
            (int)createOrEdit.Id,
            new Inclusion<Field>(fo => fo.Name.Localizations)
          ),
        createOrEdit
      );

      if (createOrEdit.Id == null)
        this.Repository.Create(field);

      else this.Repository.Edit(field);

      await this.MergeEntityLocalizationsAsync(field);
      await this.Storage.SaveAsync();
      Event<IFormEditedEventHandler, HttpContext, Form>.Broadcast(this.HttpContext, await this.GetFormAsync(field));
      return this.RedirectToAction("Index", "Forms");
    }

    return this.CreateRedirectToSelfResult();
  }

  public async Task<IActionResult> DeleteAsync(int id)
  {
    Field field = await this.Repository.GetByIdAsync(id);

    this.Repository.Delete(field.Id);
    await this.Storage.SaveAsync();
    Event<IFormEditedEventHandler, HttpContext, Form>.Broadcast(this.HttpContext, await this.GetFormAsync(field));
    return this.RedirectToAction("Index", "Forms");
  }

  private async Task<bool> IsCodeUniqueAsync(FieldFilter filter, CreateOrEditViewModel createOrEdit)
  {
    filter.Code = createOrEdit.Code;

    Field field = (await this.Repository.GetAllAsync(filter)).FirstOrDefault();

    return field == null || field.Id == createOrEdit.Id;
  }

  private async Task<Form> GetFormAsync(Field field)
  {
    return await this.Storage.GetRepository<int, Form, FormFilter>().GetByIdAsync(field.FormId);
  }
}