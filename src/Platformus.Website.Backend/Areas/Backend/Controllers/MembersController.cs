// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtCore.Events;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Parameters;
using Platformus.Website.Backend.ViewModels.Members;
using Platformus.Website.Data.Entities;
using Platformus.Website.Events;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasManageClassesPermission)]
  public class MembersController : Core.Backend.Controllers.ControllerBase
  {
    private IRepository<int, Member, MemberFilter> Repository
    {
      get => this.Storage.GetRepository<int, Member, MemberFilter>();
    }

    public MembersController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> IndexAsync([FromQuery]MemberFilter filter = null, string sorting = "+position", int offset = 0, int limit = 10)
    {
      return this.View(IndexViewModelFactory.Create(
        filter, sorting, offset, limit, await this.Repository.CountAsync(filter),
        await this.Repository.GetAllAsync(filter, sorting, offset, limit, new Inclusion<Member>(m => m.PropertyDataType), new Inclusion<Member>(m => m.RelationClass))
      ));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]MemberFilter filter, int? id)
    {
      return this.View(await CreateOrEditViewModelFactory.CreateAsync(
        this.HttpContext, filter, id == null ? null : await this.Repository.GetByIdAsync((int)id)
      ));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public async Task<IActionResult> CreateOrEditAsync([FromQuery]MemberFilter filter, CreateOrEditViewModel createOrEdit)
    {
      if (createOrEdit.Id == null && !await this.IsCodeUniqueAsync(filter, createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Member member = CreateOrEditViewModelMapper.Map(
          filter,
          createOrEdit.Id == null ? new Member() : await this.Repository.GetByIdAsync((int)createOrEdit.Id),
          createOrEdit
        );

        if (createOrEdit.Id == null)
          this.Repository.Create(member);

        else this.Repository.Edit(member);

        await this.Storage.SaveAsync();
        await this.CreateOrEditDataTypeParameterValuesAsync(member, createOrEdit.Parameters);

        if (createOrEdit.Id == null)
          Event<IMemberCreatedEventHandler, HttpContext, Member>.Broadcast(this.HttpContext, member);

        else Event<IMemberEditedEventHandler, HttpContext, Member>.Broadcast(this.HttpContext, member);

        return this.Redirect(this.Request.CombineUrl("/backend/members"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
      Member member = await this.Repository.GetByIdAsync(id);

      this.Repository.Delete(member.Id);
      await this.Storage.SaveAsync();
      Event<IMemberDeletedEventHandler, HttpContext, Member>.Broadcast(this.HttpContext, member);
      return this.Redirect(this.Request.CombineUrl("/backend/members"));
    }

    private async Task<bool> IsCodeUniqueAsync(MemberFilter filter, string code)
    {
      filter.Code = code;
      return await this.Repository.CountAsync(filter) == 0 &&
        await this.Repository.CountAsync(new MemberFilter(@class: new ClassFilter(parent: new ClassFilter(id: filter.Class.Id)), code: code)) == 0;
    }

    private async Task CreateOrEditDataTypeParameterValuesAsync(Member member, string parameters)
    {
      if (member.PropertyDataTypeId == null || string.IsNullOrEmpty(parameters))
        return;

      IRepository<int, DataTypeParameter, DataTypeParameterFilter> dataTypeParameterRepository = this.Storage.GetRepository<int, DataTypeParameter, DataTypeParameterFilter>();
      IRepository<int, DataTypeParameterValue, DataTypeParameterValueFilter> dataTypeParameterValueRepository = this.Storage.GetRepository<int, DataTypeParameterValue, DataTypeParameterValueFilter>();

      foreach (KeyValuePair<string, string> valueByCode in new ParametersParser(parameters).ParsedParameters)
      {
        DataTypeParameter dataTypeParameter = (await dataTypeParameterRepository.GetAllAsync(new DataTypeParameterFilter(dataType: new DataTypeFilter(id: (int)member.PropertyDataTypeId), code: valueByCode.Key), inclusions: new Inclusion<DataTypeParameter>(dtp => dtp.DataTypeParameterValues))).FirstOrDefault();

        if (dataTypeParameter != null)
        {
          DataTypeParameterValue dataTypeParameterValue = dataTypeParameter.DataTypeParameterValues.FirstOrDefault(dtpv => dtpv.MemberId == member.Id);

          if (dataTypeParameterValue == null)
          {
            dataTypeParameterValue = new DataTypeParameterValue();
            dataTypeParameterValue.DataTypeParameterId = dataTypeParameter.Id;
            dataTypeParameterValue.MemberId = member.Id;
            dataTypeParameterValue.Value = valueByCode.Value;
            dataTypeParameterValueRepository.Create(dataTypeParameterValue);
          }

          else
          {
            dataTypeParameterValue.Value = valueByCode.Value;
            dataTypeParameterValueRepository.Edit(dataTypeParameterValue);
          }
        }
      }

      await this.Storage.SaveAsync();
    }
  }
}