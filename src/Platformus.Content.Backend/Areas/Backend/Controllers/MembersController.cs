// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNet.Mvc;
using Platformus.Content.Backend.ViewModels.Members;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.Controllers;

namespace Platformus.Content.Backend.Controllers
{
  [Area("Backend")]
  public class MembersController : ControllerBase
  {
    public MembersController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult Index(int classId, string orderBy = "position", string direction = "asc", int skip = 0, int take = 10)
    {
      return this.View(new IndexViewModelBuilder(this).Build(classId, orderBy, direction, skip, take));
    }

    [HttpGet]
    [ImportModelStateFromTempData]
    public IActionResult CreateOrEdit(int? id, int? classId)
    {
      return this.View(new CreateOrEditViewModelBuilder(this).Build(id, classId));
    }

    [HttpPost]
    [ExportModelStateToTempData]
    public IActionResult CreateOrEdit(CreateOrEditViewModel createOrEdit)
    {
      if (this.ModelState.IsValid)
      {
        Member member = new CreateOrEditViewModelMapper(this).Map(createOrEdit);

        if (createOrEdit.Id == null)
          this.Storage.GetRepository<IMemberRepository>().Create(member);

        else this.Storage.GetRepository<IMemberRepository>().Edit(member);

        this.Storage.Save();
        return this.Redirect(this.Request.CombineUrl("/backend/members"));
      }

      return this.CreateRedirectToSelfResult();
    }

    public ActionResult Delete(int id)
    {
      Member member = this.Storage.GetRepository<IMemberRepository>().WithKey(id);

      this.Storage.GetRepository<IMemberRepository>().Delete(member);
      this.Storage.Save();
      return this.Redirect(string.Format("/backend/members?classid={0}", member.ClassId));
    }
  }
}