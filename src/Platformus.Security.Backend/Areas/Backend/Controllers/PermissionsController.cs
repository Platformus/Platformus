// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNet.Mvc;
using Platformus.Barebone.Backend.Controllers;
using Platformus.Security.Backend.ViewModels.Permissions;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Backend.Controllers
{
  [Area("Backend")]
  public class PermissionsController : ControllerBase
  {
    public PermissionsController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string orderBy = "position", string direction = "asc", int skip = 0, int take = 10)
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
        Permission permission = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IPermissionRepository>().Create(permission);

        else this.Storage.GetRepository<IPermissionRepository>().Edit(permission);

        this.Storage.Save();
        return this.Redirect(this.Request.CombineUrl("/backend/permissions"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      this.Storage.GetRepository<IPermissionRepository>().Delete(id);
      this.Storage.Save();
      return this.RedirectToAction("Index");
    }
  }
}