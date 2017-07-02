// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Domain.Backend.ViewModels.Tabs;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseClassesPermission)]
  public class TabsController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public TabsController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(int classId, string orderBy = "position", string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(classId, orderBy, direction, skip, take, filter));
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
        Tab tab = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<ITabRepository>().Create(tab);

        else this.Storage.GetRepository<ITabRepository>().Edit(tab);

        this.Storage.Save();
        return this.Redirect(this.Request.CombineUrl("/backend/tabs"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Tab tab = this.Storage.GetRepository<ITabRepository>().WithKey(id);

      this.Storage.GetRepository<ITabRepository>().Delete(tab);
      this.Storage.Save();
      return this.Redirect(string.Format("/backend/tabs?classid={0}", tab.ClassId));
    }
  }
}