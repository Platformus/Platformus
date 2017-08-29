// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Domain.Backend.ViewModels.DataTypes;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Domain.Events;

namespace Platformus.Domain.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseDataTypesPermission)]
  public class DataTypesController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public DataTypesController(IStorage storage)
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
        DataType dataType = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IDataTypeRepository>().Create(dataType);

        else this.Storage.GetRepository<IDataTypeRepository>().Edit(dataType);

        this.Storage.Save();

        if (createOrEdit.Id == null)
          Event<IDataTypeCreatedEventHandler, IRequestHandler, DataType>.Broadcast(this, dataType);

        else Event<IDataTypeEditedEventHandler, IRequestHandler, DataType>.Broadcast(this, dataType);

        return this.Redirect(this.Request.CombineUrl("/backend/datatypes"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      DataType dataType = this.Storage.GetRepository<IDataTypeRepository>().WithKey(id);

      this.Storage.GetRepository<IDataTypeRepository>().Delete(dataType);
      this.Storage.Save();
      Event<IDataTypeDeletedEventHandler, IRequestHandler, DataType>.Broadcast(this, dataType);
      return this.RedirectToAction("Index");
    }
  }
}