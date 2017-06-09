// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Designers.Backend.ViewModels.Views;

namespace Platformus.Designers.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseViewsPermission)]
  public class ViewsController : Platformus.Barebone.Backend.Controllers.ControllerBase
  {
    public ViewsController(IStorage storage)
      : base(storage)
    {
    }

    public ActionResult Index(string subdirectory, string orderBy = "filename", string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(subdirectory, orderBy, direction, skip, take, filter));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public IActionResult CreateOrEdit(string subdirectory, string id)
    {
      return this.View(new CreateOrEditViewModelFactory(this).Create(subdirectory, id));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public IActionResult CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        if (!string.IsNullOrEmpty(createOrEdit.Id))
          System.IO.File.Delete(PathManager.GetViewPath(this, createOrEdit.Subdirectory, createOrEdit.Id));

        System.IO.File.WriteAllText(PathManager.GetViewPath(this, createOrEdit.Subdirectory, createOrEdit.Filename), createOrEdit.Content);
        return this.RedirectToAction("Index", new { subdirectory = createOrEdit.Subdirectory });
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(string subdirectory, string id)
    {
      System.IO.File.Delete(PathManager.GetViewPath(this, subdirectory, id));
      return this.RedirectToAction("Index", new { subdirectory = subdirectory });
    }
  }
}