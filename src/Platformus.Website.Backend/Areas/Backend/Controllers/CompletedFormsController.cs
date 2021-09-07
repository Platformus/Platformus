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

    public async Task<IActionResult> IndexAsync([FromQuery]CompletedFormFilter filter = null, string sorting = "-created", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(
        filter, sorting, offset, limit, await this.Repository.CountAsync(filter),
        await this.Repository.GetAllAsync(filter, sorting, offset, limit, new Inclusion<CompletedForm>(cf => cf.Form.Name.Localizations))
      ));
    }

    public async Task<IActionResult> ViewAsync(int id)
    {
      return this.View(ViewViewModelFactory.Create(
        await this.Repository.GetByIdAsync(
          id,
          new Inclusion<CompletedForm>("CompletedFields.Field.FieldType"),
          new Inclusion<CompletedForm>("CompletedFields.Field.Name.Localizations")
        )
      ));
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      this.Repository.Delete(id);
      await this.Storage.SaveAsync();
      return this.Redirect(this.Request.CombineUrl("/backend/completedforms"));
    }
  }
}