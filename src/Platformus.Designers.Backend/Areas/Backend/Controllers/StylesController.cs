// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Designers.Backend.ViewModels.Styles;
using Platformus.Designers.Events;

namespace Platformus.Designers.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseStylesPermission)]
  public class StylesController : Platformus.Barebone.Backend.Controllers.ControllerBase
  {
    public StylesController(IStorage storage)
      : base(storage)
    {
    }

    public ActionResult Index(string orderBy = "filename", string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(orderBy, direction, skip, take, filter));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public IActionResult CreateOrEdit(string id)
    {
      return this.View(new CreateOrEditViewModelFactory(this).Create(id));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public IActionResult CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        if (!string.IsNullOrEmpty(createOrEdit.Id))
          System.IO.File.Delete(PathManager.GetStylePath(this, createOrEdit.Id));

        System.IO.File.WriteAllText(PathManager.GetStylePath(this, createOrEdit.Filename), createOrEdit.Content);

        if (string.IsNullOrEmpty(createOrEdit.Id))
          Event<IStyleCreatedEventHandler, IRequestHandler, string>.Broadcast(this, createOrEdit.Filename);

        else Event<IStyleEditedEventHandler, IRequestHandler, string>.Broadcast(this, createOrEdit.Filename);

        return this.RedirectToAction("Index");
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(string id)
    {
      System.IO.File.Delete(PathManager.GetStylePath(this, id));
      Event<IStyleDeletedEventHandler, IRequestHandler, string>.Broadcast(this, id);
      return this.RedirectToAction("Index");
    }
  }
}