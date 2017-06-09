// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Forms.Backend.ViewModels.CompletedForms;
using Platformus.Forms.Data.Abstractions;

namespace Platformus.Forms.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseFormsPermission)]
  public class CompletedFormsController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public CompletedFormsController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(int formId, string orderBy = "created", string direction = "desc", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelFactory(this).Create(formId, orderBy, direction, skip, take));
    }

    public IActionResult View(int id)
    {
      return this.View(new ViewViewModelFactory(this).Create(id));
    }

    public ActionResult Delete(int id)
    {
      this.Storage.GetRepository<ICompletedFormRepository>().Delete(id);
      this.Storage.Save();
      return this.RedirectToAction("Index");
    }
  }
}