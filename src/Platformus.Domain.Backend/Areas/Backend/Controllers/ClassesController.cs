// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Domain.Backend.ViewModels.Classes;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Domain.Events;

namespace Platformus.Domain.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseClassesPermission)]
  public class ClassesController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public ClassesController(IStorage storage)
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
      if (createOrEdit.Id == null && !this.IsCodeUnique(createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Class @class = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IClassRepository>().Create(@class);

        else this.Storage.GetRepository<IClassRepository>().Edit(@class);

        this.Storage.Save();

        if (createOrEdit.Id == null)
          Event<IClassCreatedEventHandler, IRequestHandler, Class>.Broadcast(this, @class);

        else Event<IClassEditedEventHandler, IRequestHandler, Class>.Broadcast(this, @class);

        return this.Redirect(this.Request.CombineUrl("/backend/classes"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Class @class = this.Storage.GetRepository<IClassRepository>().WithKey(id);

      this.Storage.GetRepository<IClassRepository>().Delete(@class);
      this.Storage.Save();
      Event<IClassDeletedEventHandler, IRequestHandler, Class>.Broadcast(this, @class);
      return this.RedirectToAction("Index");
    }

    private bool IsCodeUnique(string code)
    {
      return this.Storage.GetRepository<IClassRepository>().WithCode(code) == null;
    }
  }
}