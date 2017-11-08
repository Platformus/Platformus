// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Routing.Backend.ViewModels.DataSources;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;
using Platformus.Routing.Events;

namespace Platformus.Routing.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseEndpointsPermission)]
  public class DataSourcesController : Platformus.Barebone.Backend.Controllers.ControllerBase
  {
    public DataSourcesController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(int endpointId, string orderBy = "code", string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(endpointId, orderBy, direction, skip, take, filter));
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
      if (createOrEdit.Id == null && !this.IsCodeUnique(createOrEdit.EndpointId, createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        DataSource dataSource = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IDataSourceRepository>().Create(dataSource);

        else this.Storage.GetRepository<IDataSourceRepository>().Edit(dataSource);

        this.Storage.Save();

        if (createOrEdit.Id == null)
          Event<IDataSourceCreatedEventHandler, IRequestHandler, DataSource>.Broadcast(this, dataSource);

        else
        {
          Event<IDataSourceEditedEventHandler, IRequestHandler, DataSource, DataSource>.Broadcast(
            this, this.Storage.GetRepository<IDataSourceRepository>().WithKey((int)createOrEdit.Id), dataSource
          );
        }

        return this.Redirect(this.Request.CombineUrl("/backend/datasources"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      DataSource dataSource = this.Storage.GetRepository<IDataSourceRepository>().WithKey(id);

      this.Storage.GetRepository<IDataSourceRepository>().Delete(dataSource);
      this.Storage.Save();
      Event<IDataSourceDeletedEventHandler, IRequestHandler, DataSource>.Broadcast(this, dataSource);
      return this.Redirect(string.Format("/backend/datasources?endpointid={0}", dataSource.EndpointId));
    }

    private bool IsCodeUnique(int endpointId, string code)
    {
      return this.Storage.GetRepository<IDataSourceRepository>().WithEndpointIdAndCode(endpointId, code) == null;
    }
  }
}