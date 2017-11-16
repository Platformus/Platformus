// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Domain.Backend.ViewModels.Domain;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Backend.Controllers
{
  [Area("Backend")]
  public class DomainController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public DomainController(IStorage storage)
      : base(storage)
    {
    }

    public ActionResult ClassSelectorForm(int? classId)
    {
      return this.PartialView("_ClassSelectorForm", new ClassSelectorFormViewModelFactory(this).Create(classId));
    }

    public ActionResult MemberSelectorForm(int? memberId)
    {
      return this.PartialView("_MemberSelectorForm", new MemberSelectorFormViewModelFactory(this).Create(memberId));
    }

    public ActionResult ObjectSelectorForm(int classId, string objectIds)
    {
      return this.PartialView("_ObjectSelectorForm", new ObjectSelectorFormViewModelFactory(this).Create(classId, objectIds));
    }

    public ActionResult GetClassName(int? classId)
    {
      if (classId == null)
        return this.Content("<div class=\"class-data-source-parameter-editor__name\">Not selected</div>");

      Class @class = this.Storage.GetRepository<IClassRepository>().WithKey((int)classId);

      return this.Content(string.Format("<div class=\"class-data-source-parameter-editor__name\">{0}</div>", @class.Name));
    }

    public ActionResult GetMemberName(int? memberId)
    {
      if (memberId == null)
        return this.Content("<div class=\"class-data-source-parameter-editor__name\">Not selected</div>");

      Member member = this.Storage.GetRepository<IMemberRepository>().WithKey((int)memberId);
      Class @class = this.Storage.GetRepository<IClassRepository>().WithKey(member.ClassId);

      return this.Content(string.Format("<div class=\"member-data-source-parameter-editor__name\">{0} > {1}</div>", @class.Name, member.Name));
    }

    public ActionResult GetObjectDisplayValues(string objectIds)
    {
      StringBuilder objectDisplayValues = new StringBuilder();

      if (!string.IsNullOrEmpty(objectIds))
      {
        foreach (string objectId in objectIds.Split(','))
        {
          Object @object = this.Storage.GetRepository<IObjectRepository>().WithKey(int.Parse(objectId));

          objectDisplayValues.AppendFormat(
            "<div class=\"relation-member-editor__display-value\">{0}</div>",
            string.Join(" ", new ObjectManager(this).GetDisplayProperties(@object))
          );
        }
      }

      return this.Content(objectDisplayValues.ToString());
    }
  }
}