// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Backend.ViewModels.DataTypes;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageDataTypesPermission)]
  public class DataTypesController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, DataType, DataTypeFilter> Repository
    {
      get => this.Storage.GetRepository<int, DataType, DataTypeFilter>();
    }

    public DataTypesController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]DataTypeFilter filter = null, string orderBy = "+position", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelFactory().Create(
        this.HttpContext, filter,
        await this.Repository.GetAllAsync(filter, orderBy, skip, take),
        orderBy, skip, take, await this.Repository.CountAsync(filter)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(new CreateOrEditViewModelFactory().Create(
        id == null ? null : await this.Repository.GetByIdAsync((int)id)
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        DataType dataType = new CreateOrEditViewModelMapper().Map(
          createOrEdit.Id == null ? new DataType() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(dataType);

        else this.Repository.Edit(dataType);

        await this.Storage.SaveAsync();

        if (createOrEdit.Id == null)
          Event<IDataTypeCreatedEventHandler, HttpContext, DataType>.Broadcast(this.HttpContext, dataType);

        else Event<IDataTypeEditedEventHandler, HttpContext, DataType>.Broadcast(this.HttpContext, dataType);

        return this.Redirect(this.Request.CombineUrl("/backend/datatypes"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      DataType dataType = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(dataType.Id);
      await this.Storage.SaveAsync();
      Event<IDataTypeDeletedEventHandler, HttpContext, DataType>.Broadcast(this.HttpContext, dataType);
      return this.RedirectToAction("Index");
    }
  }
}