// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Backend.ViewModels.Credentials;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageUsersPermission)]
  public class CredentialsController : ControllerBase
  {
    private IRepository<int, Credential, CredentialFilter> Repository
    {
      get => this.Storage.GetRepository<int, Credential, CredentialFilter>();
    }

    public CredentialsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]CredentialFilter filter = null, string sorting = "+identifier", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(
        filter, sorting, offset, limit, await this.Repository.CountAsync(filter),
        await this.Repository.GetAllAsync(filter, sorting, offset, limit, new Inclusion<Credential>(c => c.CredentialType))
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(await CreateOrEditViewModelFactory.CreateAsync(
        this.HttpContext, id == null ? null : await this.Repository.GetByIdAsync((int)id)
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync(CredentialFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Credential credential = CreateOrEditViewModelMapper.Map(
          filter,
          createOrEdit.Id == null ? new Credential() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(credential);

        else this.Repository.Edit(credential);

        await this.Storage.SaveAsync();
        return this.Redirect(this.Request.CombineUrl("/backend/credentials"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Credential credential = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(credential.Id);
      await this.Storage.SaveAsync();
      return this.Redirect(this.Request.CombineUrl("/backend/credentials"));
    }
  }
}