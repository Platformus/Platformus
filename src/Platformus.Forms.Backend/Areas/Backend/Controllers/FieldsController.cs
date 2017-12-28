// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Forms.Backend.ViewModels.Fields;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;
using Platformus.Forms.Events;

namespace Platformus.Forms.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseFormsPermission)]
  public class FieldsController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public FieldsController(IStorage storage)
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
      if (createOrEdit.Id == null && !this.IsCodeUnique(createOrEdit.FormId, createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Field field = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(field);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IFieldRepository>().Create(field);

        else this.Storage.GetRepository<IFieldRepository>().Edit(field);

        this.Storage.Save();
        Event<IFormEditedEventHandler, IRequestHandler, Form>.Broadcast(this, this.GetForm(field));
        return this.RedirectToAction("Index", "Forms");
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Field field = this.Storage.GetRepository<IFieldRepository>().WithKey(id);
      Form form = this.GetForm(field);

      this.Storage.GetRepository<IFieldRepository>().Delete(field);
      this.Storage.Save();
      new SerializationManager(this).SerializeForm(form);
      return this.RedirectToAction("Index", "Forms");
    }

    private bool IsCodeUnique(int formId, string code)
    {
      return this.Storage.GetRepository<IFieldRepository>().WithFormIdAndCode(formId, code) == null;
    }

    private Form GetForm(Field field)
    {
      return this.Storage.GetRepository<IFormRepository>().WithKey(field.FormId);
    }
  }
}