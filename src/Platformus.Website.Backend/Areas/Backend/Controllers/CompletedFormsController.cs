// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Backend.ViewModels.CompletedForms;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageFormsPermission)]
  public class CompletedFormsController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, CompletedForm, CompletedFormFilter> Repository
    {
      get => this.Storage.GetRepository<int, CompletedForm, CompletedFormFilter>();
    }

    public CompletedFormsController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]CompletedFormFilter filter = null, string orderBy = "-created", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelFactory().Create(
        this.HttpContext, filter,
        await this.Repository.GetAllAsync(filter, orderBy, skip, take, new Inclusion<CompletedForm>(cf => cf.Form.Name.Localizations)),
        orderBy, skip, take, await this.Repository.CountAsync(filter)
      ));
    }

    public async Task<IActionResult> ViewAsync(int id)
    {
      return this.View(new ViewViewModelFactory().Create(
        await this.Repository.GetByIdAsync(id, new Inclusion<CompletedForm>("CompletedFields"))
      ));
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      this.Repository.Delete(id);
      await this.Storage.SaveAsync();
      return this.RedirectToAction("Index");
    }
  }
}