// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Globalization.Backend.ViewModels.Cultures;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Globalization.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseCulturesPermission)]
  public class CulturesController : ControllerBase
  {
    public CulturesController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string orderBy = "name", string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(orderBy, direction, skip, take, filter));
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
      if (this.ModelState.IsValid)
      {
        Culture culture = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<ICultureRepository>().Create(culture);

        else this.Storage.GetRepository<ICultureRepository>().Edit(culture);

        this.Storage.Save();
        CultureManager.InvalidateCache();
        return this.Redirect(this.Request.CombineUrl("/backend/cultures"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      this.Storage.GetRepository<ICultureRepository>().Delete(id);
      this.Storage.Save();
      CultureManager.InvalidateCache();
      return this.RedirectToAction("Index");
    }
  }
}