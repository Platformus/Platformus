// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNet.Mvc;
using Platformus.Content.Backend.ViewModels.Classes;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.Controllers;

namespace Platformus.Content.Backend.Controllers
{
  [Area("Backend")]
  public class ClassesController : ControllerBase
  {
    public ClassesController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string orderBy = "name", string direction = "asc", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelBuilder(this).Build(orderBy, direction, skip, take));
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
        Class @class = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IClassRepository>().Create(@class);

        else this.Storage.GetRepository<IClassRepository>().Edit(@class);

        this.Storage.Save();
        return this.Redirect(this.Request.CombineUrl("/backend/classes"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      this.Storage.GetRepository<IClassRepository>().Delete(id);
      this.Storage.Save();
      return this.RedirectToAction("Index");
    }
  }
}