// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Backend.ViewModels.DataTypeParameters;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageDataTypesPermission)]
  public class DataTypeParametersController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, DataTypeParameter, DataTypeParameterFilter> Repository
    {
      get => this.Storage.GetRepository<int, DataTypeParameter, DataTypeParameterFilter>();
    }

    public DataTypeParametersController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]DataTypeParameterFilter filter = null, string sorting = "+name", int offset = 0, int limit = 10)
    {
      return this.View(await IndexViewModelFactory.CreateAsync(
        this.HttpContext, filter, sorting, offset, limit, await this.Repository.CountAsync(filter),
        await this.Repository.GetAllAsync(filter, sorting, offset, limit)
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync(int? id)
    {
      return this.View(CreateOrEditViewModelFactory.Create(
        id == null ? null : await this.Repository.GetByIdAsync((int)id)
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]DataTypeParameterFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (createOrEdit.Id == null && !await this.IsCodeUniqueAsync(filter, createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        DataTypeParameter dataTypeParameter = CreateOrEditViewModelMapper.Map(
          filter,
          createOrEdit.Id == null ? new DataTypeParameter() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(dataTypeParameter);

        else this.Repository.Edit(dataTypeParameter);

        await this.Storage.SaveAsync();
        return this.Redirect(this.Request.CombineUrl("/backend/datatypeparameters"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      DataTypeParameter dataTypeParameter = await this.Storage.GetRepository<int, DataTypeParameter, DataTypeParameterFilter>().GetByIdAsync(id);

      this.Repository.Delete(dataTypeParameter.Id);
      await this.Storage.SaveAsync();
      return this.Redirect(this.Request.CombineUrl("/backend/datatypeparameters"));
    }

    private async Task<bool> IsCodeUniqueAsync(DataTypeParameterFilter filter, string code)
    {
      filter.Code = code;
      return await this.Repository.CountAsync(filter) == 0;
    }
  }
}