// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Forms.Backend.ViewModels.Fields;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Models;

namespace Platformus.Forms.Backend.Controllers
{
  [Area("Backend")]
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
      return this.View(new CreateOrEditViewModelBuilder(this).Build(id));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public IActionResult CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Field field = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(field);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IFieldRepository>().Create(field);

        else this.Storage.GetRepository<IFieldRepository>().Edit(field);

        this.Storage.Save();
        this.CacheForm(field);
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
      new CacheManager(this).CacheForm(form);
      return this.RedirectToAction("Index", "Forms");
    }

    private void CacheForm(Field field)
    {
      new CacheManager(this).CacheForm(this.GetForm(field));
    }

    private Form GetForm(Field field)
    {
      return this.Storage.GetRepository<IFormRepository>().WithKey(field.FormId);
    }
  }
}