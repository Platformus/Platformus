// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Domain.Backend.ViewModels.DataTypeParameters;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseDataTypesPermission)]
  public class DataTypeParametersController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public DataTypeParametersController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(int dataTypeId, string orderBy = "name", string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(dataTypeId, orderBy, direction, skip, take, filter));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public IActionResult CreateOrEdit(int? id, int? dataTypeId)
    {
      return this.View(new CreateOrEditViewModelFactory(this).Create(id, dataTypeId));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public IActionResult CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (createOrEdit.Id == null && !this.IsCodeUnique(createOrEdit.DataTypeId, createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        DataTypeParameter dataTypeParameter = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IDataTypeParameterRepository>().Create(dataTypeParameter);

        else this.Storage.GetRepository<IDataTypeParameterRepository>().Edit(dataTypeParameter);

        this.Storage.Save();
        return this.Redirect(this.Request.CombineUrl("/backend/datatypeparameters"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      DataTypeParameter dataTypeParameter = this.Storage.GetRepository<IDataTypeParameterRepository>().WithKey(id);

      this.Storage.GetRepository<IDataTypeParameterRepository>().Delete(dataTypeParameter);
      this.Storage.Save();
      return this.Redirect(string.Format("/backend/datatypeparameters?datatypeid={0}", dataTypeParameter.DataTypeId));
    }

    private bool IsCodeUnique(int dataTypeId, string code)
    {
      return this.Storage.GetRepository<IDataTypeParameterRepository>().WithDataTypeIdAndCode(dataTypeId, code) == null;
    }
  }
}