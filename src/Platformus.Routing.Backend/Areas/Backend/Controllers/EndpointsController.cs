// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Routing.Backend.ViewModels.Endpoints;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;
using Platformus.Routing.Events;

namespace Platformus.Routing.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseEndpointsPermission)]
  public class EndpointsController : Platformus.Barebone.Backend.Controllers.ControllerBase
  {
    public EndpointsController(IStorage storage)
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
        Endpoint endpoint = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IEndpointRepository>().Create(endpoint);

        else this.Storage.GetRepository<IEndpointRepository>().Edit(endpoint);

        this.Storage.Save();
        this.CreateOrEditEndpointPermissions(endpoint);

        if (createOrEdit.Id == null)
          Event<IEndpointCreatedEventHandler, IRequestHandler, Endpoint>.Broadcast(this, endpoint);

        else
        {
          Event<IEndpointEditedEventHandler, IRequestHandler, Endpoint, Endpoint>.Broadcast(
            this, this.Storage.GetRepository<IEndpointRepository>().WithKey((int)createOrEdit.Id), endpoint
          );
        }

        return this.Redirect(this.Request.CombineUrl("/backend/endpoints"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Endpoint endpoint = this.Storage.GetRepository<IEndpointRepository>().WithKey(id);

      this.Storage.GetRepository<IEndpointRepository>().Delete(id);
      this.Storage.Save();
      Event<IEndpointDeletedEventHandler, IRequestHandler, Endpoint>.Broadcast(this, endpoint);
      return this.RedirectToAction("Index");
    }

    private void CreateOrEditEndpointPermissions(Endpoint endpoint)
    {
      this.DeleteEndpointPermissions(endpoint);
      this.CreateEndpointPermissions(endpoint);
    }

    private void DeleteEndpointPermissions(Endpoint endpoint)
    {
      foreach (EndpointPermission endpointPermission in this.Storage.GetRepository<IEndpointPermissionRepository>().FilteredByEndpointId(endpoint.Id))
        this.Storage.GetRepository<IEndpointPermissionRepository>().Delete(endpointPermission);

      this.Storage.Save();
    }

    private void CreateEndpointPermissions(Endpoint endpoint)
    {
      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("permission") && this.Request.Form[key] == true.ToString().ToLower())
        {
          string permissionId = key.Replace("permission", string.Empty);

          this.CreateEndpointPermission(endpoint, int.Parse(permissionId));
        }
      }

      this.Storage.Save();
    }

    private void CreateEndpointPermission(Endpoint endpoint, int permissionId)
    {
      EndpointPermission endpointPermission = new EndpointPermission();

      endpointPermission.EndpointId = endpoint.Id;
      endpointPermission.PermissionId = permissionId;
      this.Storage.GetRepository<IEndpointPermissionRepository>().Create(endpointPermission);
    }
  }
}