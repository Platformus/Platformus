// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend;
using Platformus.Website.Backend.ViewModels.Endpoints;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers;

[Authorize(Policy = Policies.HasManageEndpointsPermission)]
public class EndpointsController : Core.Backend.Controllers.ControllerBase
{
  private IStringLocalizer localizer;

  private IRepository<int, Data.Entities.Endpoint, EndpointFilter> EndpointRepository
  {
    get => this.Storage.GetRepository<int, Data.Entities.Endpoint, EndpointFilter>();
  }

  private IRepository<int, int, EndpointPermission, EndpointPermissionFilter> EndpointPermissionRepository
  {
    get => this.Storage.GetRepository<int, int, EndpointPermission, EndpointPermissionFilter>();
  }

  public EndpointsController(IStorage storage, IStringLocalizer<SharedResource> localizer)
    : base(storage)
  {
    this.localizer = localizer;
  }

  public async Task<IActionResult> IndexAsync([FromQuery] EndpointFilter filter = null, string sorting = "+position", int offset = 0, int limit = 10)
  {
    return this.View(IndexViewModelFactory.Create(
      sorting, offset, limit, await this.EndpointRepository.CountAsync(filter),
      await this.EndpointRepository.GetAllAsync(filter, sorting, offset, limit)
    ));
  }

  [HttpGet]
  [ImportModelStateFromTempData]
  public async Task<IActionResult> CreateOrEditAsync(int? id)
  {
    return this.View(await CreateOrEditViewModelFactory.CreateAsync(
      this.HttpContext, id == null ? null : await this.EndpointRepository.GetByIdAsync((int)id, new Inclusion<Data.Entities.Endpoint>(e => e.EndpointPermissions))
    ));
  }

  [HttpPost]
  [ExportModelStateToTempData]
  public async Task<IActionResult> CreateOrEditAsync(CreateOrEditViewModel createOrEdit)
  {
    if (!await this.IsUrlTemplateUniqueAsync(createOrEdit))
      this.ModelState.AddModelError("urlTemplate", this.localizer["Value is already in use"]);

    if (this.ModelState.IsValid)
    {
      Data.Entities.Endpoint endpoint = CreateOrEditViewModelMapper.Map(
        createOrEdit.Id == null ? new Data.Entities.Endpoint() : await this.EndpointRepository.GetByIdAsync((int)createOrEdit.Id, new Inclusion<Data.Entities.Endpoint>(e => e.EndpointPermissions)),
        createOrEdit
      );

      if (createOrEdit.Id == null)
        this.EndpointRepository.Create(endpoint);

      else this.EndpointRepository.Edit(endpoint);

      this.MergeEndpointPermissions(endpoint);
      await this.Storage.SaveAsync();

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
    return this.Redirect(this.Request.CombineUrl("/backend/endpoints"));
  }

  private async Task<bool> IsUrlTemplateUniqueAsync(CreateOrEditViewModel createOrEdit)
  {
    StringFilter urlTemplate = new StringFilter();

    if (string.IsNullOrEmpty(createOrEdit.UrlTemplate))
      urlTemplate.IsNull = true;

    else urlTemplate.Equals = createOrEdit.UrlTemplate;

    Data.Entities.Endpoint endpoint = (await this.EndpointRepository.GetAllAsync(new EndpointFilter(urlTemplate: urlTemplate))).FirstOrDefault();

    return endpoint == null || endpoint.Id == createOrEdit.Id;
  }

  private void MergeEndpointPermissions(Data.Entities.Endpoint endpoint)
  {
    List<int> permissionIds = new List<int>();

    foreach (string key in this.Request.Form.Keys)
      if (key.StartsWith("permission") && this.Request.Form[key].FirstOrDefault().ToBoolWithDefaultValue(false))
        permissionIds.Add(int.Parse(key.Replace("permission", string.Empty)));

    if (!endpoint.DisallowAnonymous)
      permissionIds.Clear();

    IEnumerable<EndpointPermission> currentEndpointPermissions = endpoint.EndpointPermissions ?? Array.Empty<EndpointPermission>();

    foreach (EndpointPermission endpointPermission in currentEndpointPermissions.Where(cep => !permissionIds.Any(id => id == cep.PermissionId)).ToList())
      this.EndpointPermissionRepository.Delete(endpointPermission.EndpointId, endpointPermission.PermissionId);

    foreach (int permissionId in permissionIds.Where(id => !currentEndpointPermissions.Any(cep => cep.PermissionId == id)))
      this.EndpointPermissionRepository.Create(new EndpointPermission() { Endpoint = endpoint, PermissionId = permissionId });
  }
}