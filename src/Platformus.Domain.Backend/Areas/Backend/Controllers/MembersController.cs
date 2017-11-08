// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone;
using Platformus.Domain.Backend.ViewModels.Members;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Domain.Events;

namespace Platformus.Domain.Backend.Controllers
{
  [Area("Backend")]
  [Authorize(Policy = Policies.HasBrowseClassesPermission)]
  public class MembersController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public MembersController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(int classId, string orderBy = "position", string direction = "asc", int skip = 0, int take = 10, string filter = null)
    {
      return this.View(new IndexViewModelFactory(this).Create(classId, orderBy, direction, skip, take, filter));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public IActionResult CreateOrEdit(int? id, int? classId)
    {
      return this.View(new CreateOrEditViewModelFactory(this).Create(id, classId));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public IActionResult CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (createOrEdit.Id == null && !this.IsCodeUnique(createOrEdit.ClassId, createOrEdit.Code))
        this.ModelState.AddModelError("code", string.Empty);

      if (this.ModelState.IsValid)
      {
        Member member = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IMemberRepository>().Create(member);

        else this.Storage.GetRepository<IMemberRepository>().Edit(member);

        this.Storage.Save();
        this.CreateOrEditDataTypeParameterValues(member);

        if (createOrEdit.Id == null)
          Event<IMemberCreatedEventHandler, IRequestHandler, Member>.Broadcast(this, member);

        else Event<IMemberEditedEventHandler, IRequestHandler, Member>.Broadcast(this, member);

        return this.Redirect(this.Request.CombineUrl("/backend/members"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Member member = this.Storage.GetRepository<IMemberRepository>().WithKey(id);

      this.Storage.GetRepository<IMemberRepository>().Delete(member);
      this.Storage.Save();
      Event<IMemberDeletedEventHandler, IRequestHandler, Member>.Broadcast(this, member);
      return this.Redirect(string.Format("/backend/members?classid={0}", member.ClassId));
    }

    private bool IsCodeUnique(int classId, string code)
    {
      return this.Storage.GetRepository<IMemberRepository>().WithClassIdAndCodeInlcudingParent(classId, code) == null;
    }

    private void CreateOrEditDataTypeParameterValues(Member member)
    {
      IDataTypeParameterValueRepository dataTypeParameterValueRepository = this.Storage.GetRepository<IDataTypeParameterValueRepository>();

      foreach (string key in this.Request.Form.Keys)
      {
        if (key.StartsWith("dataTypeParameter"))
        {
          int dataTypeParameterId = int.Parse(key.Replace("dataTypeParameter", string.Empty));
          DataTypeParameterValue dataTypeParameterValue = dataTypeParameterValueRepository.WithDataTypeParameterIdAndMemberId(dataTypeParameterId, member.Id);

          if (dataTypeParameterValue == null)
          {
            dataTypeParameterValue = new DataTypeParameterValue();
            dataTypeParameterValue.DataTypeParameterId = dataTypeParameterId;
            dataTypeParameterValue.MemberId = member.Id;
            dataTypeParameterValue.Value = this.Request.Form[key];
            dataTypeParameterValueRepository.Create(dataTypeParameterValue);
          }

          else
          {
            dataTypeParameterValue.Value = this.Request.Form[key];
            dataTypeParameterValueRepository.Edit(dataTypeParameterValue);
          }
        }
      }

      this.Storage.Save();
    }
  }
}