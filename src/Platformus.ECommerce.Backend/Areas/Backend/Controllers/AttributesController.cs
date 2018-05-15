// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.ECommerce.Backend.ViewModels.Attributes;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseFeaturesPermission)]
  public class AttributesController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public AttributesController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(int featureId, string orderBy = "position", string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(featureId, orderBy, direction, skip, take, filter));
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
        Attribute attribute = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        this.CreateOrEditEntityLocalizations(attribute);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IAttributeRepository>().Create(attribute);

        else this.Storage.GetRepository<IAttributeRepository>().Edit(attribute);

        this.Storage.Save();
        return this.Redirect(this.Request.CombineUrl("/backend/attributes"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Attribute attribute = this.Storage.GetRepository<IAttributeRepository>().WithKey(id);

      this.Storage.GetRepository<IAttributeRepository>().Delete(attribute);
      this.Storage.Save();
      return this.Redirect(string.Format("/backend/attributes?featureid={0}", attribute.FeatureId));
    }
  }
}