// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Primitives;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Members;

public static class CreateOrEditViewModelFactory
{
  public static async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, MemberFilter filter, Member member)
  {
    if (member == null)
      return new CreateOrEditViewModel()
      {
        TabOptions = await GetTabOptionsAsync(httpContext, (int)filter.Class.Id),
        PropertyDataTypeOptions = await GetPropertyDataTypeOptionsAsync(httpContext),
        RelationClassOptions = await GetRelationClassOptionsAsync(httpContext)
      };

    return new CreateOrEditViewModel()
    {
      Id = member.Id,
      TabId = member.TabId,
      TabOptions = await GetTabOptionsAsync(httpContext, member.ClassId),
      Code = member.Code,
      Name = member.Name,
      Position = member.Position,
      PropertyDataTypeId = member.PropertyDataTypeId,
      PropertyDataTypeOptions = await GetPropertyDataTypeOptionsAsync(httpContext),
      IsPropertyLocalizable = member.IsPropertyLocalizable == true,
      IsPropertyVisibleInList = member.IsPropertyVisibleInList == true,
      PropertyDataTypeParameters = member.PropertyDataTypeParameters,
      RelationClassId = member.RelationClassId,
      RelationClassOptions = await GetRelationClassOptionsAsync(httpContext),
      IsRelationSingleParent = member.IsRelationSingleParent == true,
      MinRelatedObjectsNumber = member.MinRelatedObjectsNumber,
      MaxRelatedObjectsNumber = member.MaxRelatedObjectsNumber
    };
  }

  private static async Task<IEnumerable<Option>> GetTabOptionsAsync(HttpContext httpContext, int classId)
  {
    IStringLocalizer localizer = httpContext.GetStringLocalizer<CreateOrEditViewModel>();
    List<Option> options = new List<Option>();

    options.Add(new Option(localizer["Tab not specified"], string.Empty));
    options.AddRange(
      (await httpContext.GetStorage().GetRepository<int, Tab, TabFilter>().GetAllAsync(new TabFilter(@class: new ClassFilter(id: classId)))).Select(
        t => new Option(t.Name, t.Id.ToString())
      )
    );

    return options;
  }

  private static async Task<IEnumerable<Option>> GetPropertyDataTypeOptionsAsync(HttpContext httpContext)
  {
    IStringLocalizer localizer = httpContext.GetStringLocalizer<CreateOrEditViewModel>();
    List<Option> options = new List<Option>();

    options.Add(new Option(localizer["Property data type not specified"], string.Empty));
    options.AddRange(
      (await httpContext.GetStorage().GetRepository<int, DataType, DataTypeFilter>().GetAllAsync(sorting: "+position")).Select(
        dt => new Option(dt.Name, dt.Id.ToString())
      )
    );

    return options;
  }

  private static async Task<IEnumerable<Option>> GetRelationClassOptionsAsync(HttpContext httpContext)
  {
    IStringLocalizer localizer = httpContext.GetStringLocalizer<CreateOrEditViewModel>();
    List<Option> options = new List<Option>();

    options.Add(new Option(localizer["Relation class not specified"], string.Empty));
    options.AddRange(
      (await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetAllAsync()).Select(
        c => new Option(c.Name, c.Id.ToString())
      )
    );

    return options;
  }
}