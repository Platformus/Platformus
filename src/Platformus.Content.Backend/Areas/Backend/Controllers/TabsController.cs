// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Content.Backend.ViewModels.Tabs;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;

namespace Platformus.Content.Backend.Controllers
{
  [Area("Backend")]
  public class TabsController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public TabsController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(int classId, string orderBy = "position", string direction = "asc", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelFactory(this).Create(classId, orderBy, direction, skip, take));
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