// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Backend.ViewModels.DataSources;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageEndpointsPermission)]
  public class DataSourcesController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, DataSource, DataSourceFilter> Repository
    {
      get => this.Storage.GetRepository<int, DataSource, DataSourceFilter>();
    }

    public DataSourcesController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]DataSourceFilter filter = null, string orderBy = "+code", int skip = 0, int take = 10)
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
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]DataSourceFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (createOrEdit.Id == null && !await this.IsCodeUniqueAsync(filter, createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        DataSource dataSource = new CreateOrEditViewModelMapper().Map(
          filter,
          createOrEdit.Id == null ? new DataSource() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(dataSource);

        else this.Repository.Edit(dataSource);

        await this.Storage.SaveAsync();

        if (createOrEdit.Id == null)
          Event<IDataSourceCreatedEventHandler, HttpContext, DataSource>.Broadcast(this.HttpContext, dataSource);

        else Event<IDataSourceEditedEventHandler, HttpContext, DataSource, DataSource>.Broadcast(this.HttpContext, await this.Repository.GetByIdAsync((int)createOrEdit.Id), dataSource);

        return this.Redirect(this.Request.CombineUrl("/backend/datasources"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      DataSource dataSource = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(dataSource.Id);
      await this.Storage.SaveAsync();
      Event<IDataSourceDeletedEventHandler, HttpContext, DataSource>.Broadcast(this.HttpContext, dataSource);
      return this.Redirect(string.Format("/backend/datasources?endpoint.id={0}", dataSource.EndpointId));
    }

    private async Task<bool> IsCodeUniqueAsync(DataSourceFilter filter, string code)
    {
      filter.Code = code;
      return await this.Repository.CountAsync(filter) == 0;
    }
  }
}