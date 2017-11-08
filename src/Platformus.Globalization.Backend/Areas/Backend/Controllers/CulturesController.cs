// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels.Cultures;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;
using Platformus.Globalization.Events;

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
      if (createOrEdit.Id == null && !this.IsCodeUnique(createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Culture culture = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<ICultureRepository>().Create(culture);

        else this.Storage.GetRepository<ICultureRepository>().Edit(culture);

        this.Storage.Save();

        if (createOrEdit.Id == null)
          Event<ICultureCreatedEventHandler, IRequestHandler, Culture>.Broadcast(this, culture);

        else Event<ICultureEditedEventHandler, IRequestHandler, Culture>.Broadcast(this, culture);

        return this.Redirect(this.Request.CombineUrl("/backend/cultures"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Culture culture = this.Storage.GetRepository<ICultureRepository>().WithKey(id);

      this.Storage.GetRepository<ICultureRepository>().Delete(culture);
      this.Storage.Save();
      Event<ICultureDeletedEventHandler, IRequestHandler, Culture>.Broadcast(this, culture);
      return this.RedirectToAction("Index");
    }

    private bool IsCodeUnique(string code)
    {
      return this.Storage.GetRepository<ICultureRepository>().WithCode(code) == null;
    }
  }
}