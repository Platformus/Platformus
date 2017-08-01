// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Domain.Backend.ViewModels.Microcontrollers;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseMicrocontrollersPermission)]
  public class MicrocontrollersController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public MicrocontrollersController(IStorage storage)
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
      if (this.ModelState.IsValid)
      {
        Microcontroller microcontroller = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IMicrocontrollerRepository>().Create(microcontroller);

        else this.Storage.GetRepository<IMicrocontrollerRepository>().Edit(microcontroller);

        this.Storage.Save();

        if (createOrEdit.Id == null)
          Event<IMicrocontrollerCreatedEventHandler, IRequestHandler, Microcontroller>.Broadcast(this, microcontroller);

        else
        {
          Event<IMicrocontrollerEditedEventHandler, IRequestHandler, Microcontroller, Microcontroller>.Broadcast(
            this, this.Storage.GetRepository<IMicrocontrollerRepository>().WithKey((int)createOrEdit.Id), microcontroller
          );
        }

        return this.Redirect(this.Request.CombineUrl("/backend/microcontrollers"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Microcontroller microcontroller = this.Storage.GetRepository<IMicrocontrollerRepository>().WithKey(id);

      this.Storage.GetRepository<IMicrocontrollerRepository>().Delete(id);
      this.Storage.Save();
      Event<IMicrocontrollerDeletedEventHandler, IRequestHandler, Microcontroller>.Broadcast(this, microcontroller);
      return this.RedirectToAction("Index");
    }
  }
}