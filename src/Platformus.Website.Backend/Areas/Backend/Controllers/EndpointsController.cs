// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Backend.ViewModels.Endpoints;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageEndpointsPermission)]
  public class EndpointsController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, Data.Entities.Endpoint, EndpointFilter> EndpointRepository
    {
      get => this.Storage.GetRepository<int, Data.Entities.Endpoint, EndpointFilter>();
    }

    private IRepository<int, int, Data.Entities.EndpointPermission, EndpointPermissionFilter> EndpointPermissionRepository
    {
      get => this.Storage.GetRepository<int, int, Data.Entities.EndpointPermission, EndpointPermissionFilter>();
    }

    public EndpointsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]EndpointFilter filter = null, string orderBy = "+position", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelFactory().Create(
        this.HttpContext, filter,
        await this.EndpointRepository.GetAllAsync(filter, orderBy, skip, take),
        orderBy, skip, take, await this.EndpointRepository.CountAsync(filter)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(await new CreateOrEditViewModelFactory().CreateAsync(
        this.HttpContext, id == null? null : await this.EndpointRepository.GetByIdAsync((int)id, new Inclusion<Data.Entities.Endpoint>(e => e.EndpointPermissions))
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Data.Entities.Endpoint endpoint = new CreateOrEditViewModelMapper().Map(
          createOrEdit.Id == null ? new Data.Entities.Endpoint() : await this.EndpointRepository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.EndpointRepository.Create(endpoint);

        else this.EndpointRepository.Edit(endpoint);

        await this.Storage.SaveAsync();
        await this.CreateOrEditEndpointPermissionsAsync(endpoint);

        if (createOrEdit.Id == null)
          Event<IEndpointCreatedEventHandler, HttpContext, Data.Entities.Endpoint>.Broadcast(this.HttpContext, endpoint);

        else Event<IEndpointEditedEventHandler, HttpContext, Data.Entities.Endpoint, Data.Entities.Endpoint>.Broadcast(this.HttpContext, await this.EndpointRepository.GetByIdAsync((int)createOrEdit.Id), endpoint);

        return this.Redirect(this.Request.CombineUrl("/backend/endpoints"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Data.Entities.Endpoint endpoint = await this.EndpointRepository.GetByIdAsync(id);

      this.EndpointRepository.Delete(id);
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
      if (endpoint.EndpointPermissions != null)
        for (int i = 0; i != endpoint.EndpointPermissions.Count; i++)
        {
          EndpointPermission endpointPermission = endpoint.EndpointPermissions.ToArray()[i];

          this.EndpointPermissionRepository.Delete(endpointPermission.EndpointId, endpointPermission.PermissionId);
        }

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
      this.EndpointPermissionRepository.Create(endpointPermission);
    }
  }
}