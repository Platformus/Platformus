// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Credentials;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.Controllers
{
  [Authorize(Policy = Policies.HasManageUsersPermission)]
  public class CredentialsController : ControllerBase
  {
    private IStringLocalizer localizer;

    private IRepository<int, Credential, CredentialFilter> Repository
    {
      get => this.Storage.GetRepository<int, Credential, CredentialFilter>();
    }

    public CredentialsController(IStorage storage, IStringLocalizer<SharedResource> localizer)
      : base(storage)
    {
      this.localizer = localizer;
    }

    public async Task<IActionResult> IndexAsync([FromQuery]CredentialFilter filter = null, string sorting = "+identifier", int offset = 0, int limit = 10)
    {
      return this.View(await IndexViewModelFactory.CreateAsync(
        this.HttpContext, filter, sorting, offset, limit, await this.Repository.CountAsync(filter),
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
      if (!await this.IsIdentifierUniqueAsync(createOrEdit))
        this.ModelState.AddModelError("identifier", this.localizer["Value is already in use"]);

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

    private async Task<bool> IsIdentifierUniqueAsync(CreateOrEditViewModel createOrEdit)
    {
      Credential credential = (await this.Repository.GetAllAsync(new CredentialFilter(credentialType: new CredentialTypeFilter(id: createOrEdit.CredentialTypeId), identifier: new StringFilter(equals: createOrEdit.Identifier)))).FirstOrDefault();

      return credential == null || credential.Id == createOrEdit.Id;
    }
  }
}