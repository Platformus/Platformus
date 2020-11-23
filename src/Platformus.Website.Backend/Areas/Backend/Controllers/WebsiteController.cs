// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Website.Backend.ViewModels.Website;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.Controllers
{
  [Area("Backend")]
  public class WebsiteController : Core.Backend.Controllers.ControllerBase
  {
    public WebsiteController(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IActionResult> ClassSelectorFormAsync(int? classId)
    {
      return this.PartialView("_ClassSelectorForm", new ClassSelectorFormViewModelFactory().Create(
        this.HttpContext,
        await this.Storage.GetRepository<int, Class, ClassFilter>().GetAllAsync(
          inclusions: new Inclusion<Class>(c => c.Parent)
        ),
        classId
      ));
    }

    public async Task<IActionResult> Class(int id)
    {
      return this.Json(await this.Storage.GetRepository<int, Class, ClassFilter>().GetByIdAsync(id));
    }

    public async Task<IActionResult> MemberSelectorFormAsync(int? memberId)
    {
      return this.PartialView("_MemberSelectorForm", new MemberSelectorFormViewModelFactory().Create(
        this.HttpContext,
        await this.Storage.GetRepository<int, Class, ClassFilter>().GetAllAsync(
          inclusions: new Inclusion<Class>(c => c.Members)
        ),
        memberId
      ));
    }

    public async Task<IActionResult> Member(int id)
    {
      return this.Json(await this.Storage.GetRepository<int, Member, MemberFilter>().GetByIdAsync(
        id,
        new Inclusion<Member>(m => m.Class)
      ));
    }

    public async Task<IActionResult> ObjectSelectorFormAsync([FromQuery]ObjectFilter filter, string objectIds)
    {
      return this.PartialView("_ObjectSelectorForm", await new ObjectSelectorFormViewModelFactory().CreateAsync(
        this.HttpContext,
        filter,
        await this.Storage.GetRepository<int, Object, ObjectFilter>().GetAllAsync(
          filter,
          inclusions: new Inclusion<Object>[]
          {
            new Inclusion<Object>("Properties.Member"),
            new Inclusion<Object>("Properties.StringValue.Localizations")
          }
        ),
        objectIds
      ));
    }

    public async Task<IActionResult> DisplayableObjects(string ids)
    {
      List<dynamic> objects = new List<dynamic>();
      IRepository<int, Object, ObjectFilter> repository = this.Storage.GetRepository<int, Object, ObjectFilter>();

      if (!string.IsNullOrEmpty(ids))
      {
        foreach (int id in ids.Split(',').Select(id => int.Parse(id)))
        {
          Object @object = await repository.GetByIdAsync(
            id,
            new Inclusion<Object>("Properties.Member"),
            new Inclusion<Object>("Properties.StringValue.Localizations")
          );

          objects.Add(@object.ToDisplayable(HttpContext));
        }
      }

      return this.Json(objects);
    }

    public async Task<IActionResult> FileSelectorForm()
    {
      return this.PartialView("_FileSelectorForm", new FileSelectorFormViewModelFactory().Create(
        await this.Storage.GetRepository<int, File, FileFilter>().GetAllAsync()
      ));
    }
  }
}