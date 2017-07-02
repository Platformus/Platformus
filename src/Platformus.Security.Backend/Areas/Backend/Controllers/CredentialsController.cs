// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Security.Backend.ViewModels.Credentials;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseUsersPermission)]
  public class CredentialsController : Barebone.Backend.Controllers.ControllerBase
  {
    public CredentialsController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(int userId, string orderBy = "identifier", string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(userId, orderBy, direction, skip, take, filter));
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
        Credential credential = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<ICredentialRepository>().Create(credential);

        else this.Storage.GetRepository<ICredentialRepository>().Edit(credential);

        this.Storage.Save();
        return this.Redirect(this.Request.CombineUrl("/backend/credentials"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Credential credential = this.Storage.GetRepository<ICredentialRepository>().WithKey(id);

      this.Storage.GetRepository<ICredentialRepository>().Delete(credential);
      this.Storage.Save();
      return this.Redirect(string.Format("/backend/credentials?userid={0}", credential.UserId));
    }
  }
}