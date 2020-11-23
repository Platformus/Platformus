// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Backend.ViewModels.Endpoints;
using Platformus.Website.Data.Abstractions;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageEndpointsPermission)]
  public class EndpointsController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, Data.Entities.Endpoint, EndpointFilter> Repository
    {
      get => this.Storage.GetRepository<int, Data.Entities.Endpoint, EndpointFilter>();
    }

    public EndpointsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]EndpointFilter filter = null, string orderBy = "+position", int skip = 0, int take = 10)
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
      return this.View(await new CreateOrEditViewModelFactory().CreateAsync(
        this.HttpContext, id == null? null : await this.Repository.GetByIdAsync((int)id)
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Data.Entities.Endpoint endpoint = new CreateOrEditViewModelMapper().Map(
          createOrEdit.Id == null ? new Data.Entities.Endpoint() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(endpoint);

        else this.Repository.Edit(endpoint);

        await this.Storage.SaveAsync();
        await this.CreateOrEditEndpointPermissionsAsync(endpoint);

        if (createOrEdit.Id == null)
          Event<IEndpointCreatedEventHandler, HttpContext, Data.Entities.Endpoint>.Broadcast(this.HttpContext, endpoint);

        else Event<IEndpointEditedEventHandler, HttpContext, Data.Entities.Endpoint, Data.Entities.Endpoint>.Broadcast(this.HttpContext, await this.Repository.GetByIdAsync((int)createOrEdit.Id), endpoint);

        return this.Redirect(this.Request.CombineUrl("/backend/endpoints"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Data.Entities.Endpoint endpoint = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(id);
      await this.Storage.SaveAsync();
      Event<IEndpointDeletedEventHandler, HttpContext, Data.Entities.Endpoint>.Broadcast(this.HttpContext, endpoint);
      return this.RedirectToAction("Index");
    }

    private async Task CreateOrEditEndpointPermissionsAsync(Data.Entities.Endpoint endpoint)
    {
      await this.DeleteEndpointPermissionsAsync(endpoint);
      await this.CreateEndpointPermissionsAsync(endpoint);
    }

    private async Task DeleteEndpointPermissionsAsync(Data.Entities.Endpoint endpoint)
    {
      foreach (EndpointPermission endpointPermission in this.Storage.GetRepository<IEndpointPermissionRepository>().FilteredByEndpointId(endpoint.Id))
        this.Storage.GetRepository<IEndpointPermissionRepository>().Delete(endpointPermission);

      await this.Storage.SaveAsync();
    }

    private async Task CreateEndpointPermissionsAsync(Data.Entities.Endpoint endpoint)
    {
      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("permission") && this.Request.Form[key] == true.ToString().ToLower())
        {
          string permissionId = key.Replace("permission", string.Empty);

          this.CreateEndpointPermission(endpoint, int.Parse(permissionId));
        }
      }

      await this.Storage.SaveAsync();
    }

    private void CreateEndpointPermission(Data.Entities.Endpoint endpoint, int permissionId)
    {
      EndpointPermission endpointPermission = new EndpointPermission();

      endpointPermission.EndpointId = endpoint.Id;
      endpointPermission.PermissionId = permissionId;
      this.Storage.GetRepository<IEndpointPermissionRepository>().Create(endpointPermission);
    }
  }
}