// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNet.Mvc;
using Platformus.Forms.Backend.ViewModels.FieldOptions;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Models;
using Platformus.Globalization.Backend.Controllers;

namespace Platformus.Forms.Backend.Controllers
{
  [Area("Backend")]
  public class FieldOptionsController : ControllerBase
  {
    public FieldOptionsController(IStorage storage)
      : base(storage)
    {
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public IActionResult CreateOrEdit(int? id)
    {
      return this.View(new CreateOrEditViewModelBuilder(this).Build(id));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public IActionResult CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        FieldOption fieldOption = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(fieldOption);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IFieldOptionRepository>().Create(fieldOption);

        else this.Storage.GetRepository<IFieldOptionRepository>().Edit(fieldOption);

        this.Storage.Save();
        this.CacheForm(fieldOption);
        return this.RedirectToAction("Index", "Forms");
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      FieldOption fieldOption = this.Storage.GetRepository<IFieldOptionRepository>().WithKey(id);
      Form form = this.GetForm(fieldOption);

      this.Storage.GetRepository<IFieldOptionRepository>().Delete(fieldOption);
      this.Storage.Save();
      new CacheManager(this).CacheForm(form);
      return this.RedirectToAction("Index", "Forms");
    }

    private void CacheForm(FieldOption fieldOption)
    {
      new CacheManager(this).CacheForm(this.GetForm(fieldOption));
    }

    private Form GetForm(FieldOption fieldOption)
    {
      Field field = this.Storage.GetRepository<IFieldRepository>().WithKey(fieldOption.FieldId);

      return this.Storage.GetRepository<IFormRepository>().WithKey(field.FormId);
    }
  }
}