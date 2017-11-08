// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Security.Backend.ViewModels.Permissions;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;
using Platformus.Security.Events;

namespace Platformus.Security.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowsePermissionsPermission)]
  public class PermissionsController : Barebone.Backend.Controllers.ControllerBase
  {
    public PermissionsController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(string orderBy = "position", string direction = "asc", int skip = 0, int take = 10, string filter = null)
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
      if (createOrEdit.Id == null && !this.IsCodeUnique(createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Permission permission = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IPermissionRepository>().Create(permission);

        else this.Storage.GetRepository<IPermissionRepository>().Edit(permission);

        this.Storage.Save();

        if (createOrEdit.Id == null)
          Event<IPermissionCreatedEventHandler, IRequestHandler, Permission>.Broadcast(this, permission);

        else Event<IPermissionEditedEventHandler, IRequestHandler, Permission>.Broadcast(this, permission);

        return this.Redirect(this.Request.CombineUrl("/backend/permissions"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Permission permission = this.Storage.GetRepository<IPermissionRepository>().WithKey(id);

      this.Storage.GetRepository<IPermissionRepository>().Delete(permission);
      this.Storage.Save();
      Event<IPermissionDeletedEventHandler, IRequestHandler, Permission>.Broadcast(this, permission);
      return this.RedirectToAction("Index");
    }

    private bool IsCodeUnique(string code)
    {
      return this.Storage.GetRepository<IPermissionRepository>().WithCode(code) == null;
    }
  }
}