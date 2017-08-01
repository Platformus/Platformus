// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Domain.Backend.ViewModels.DataSources;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseMicrocontrollersPermission)]
  public class DataSourcesController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public DataSourcesController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(int microcontrollerId, string orderBy = "code", string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(microcontrollerId, orderBy, direction, skip, take, filter));
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
      return this.Redirect(string.Format("/backend/datasources?microcontrollerid={0}", dataSource.MicrocontrollerId));
    }
  }
}