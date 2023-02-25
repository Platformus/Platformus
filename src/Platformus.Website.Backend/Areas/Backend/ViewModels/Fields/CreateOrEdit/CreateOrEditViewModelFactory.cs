// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Primitives;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Fields;

public static class CreateOrEditViewModelFactory
{
  public static async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, Field field)
  {
    if (field == null)
      return new CreateOrEditViewModel()
      {
        FieldTypeOptions = await GetFieldTypeOptionsAsync(httpContext),
        NameLocalizations = httpContext.GetLocalizations()
      };

    return new CreateOrEditViewModel()
    {
      Id = field.Id,
      FieldTypeId = field.FieldTypeId,
      FieldTypeOptions = await GetFieldTypeOptionsAsync(httpContext),
      Code = field.Code,
      NameLocalizations = httpContext.GetLocalizations(field.Name),
      IsRequired = field.IsRequired,
      MaxLength = field.MaxLength,
      Position = field.Position
    };
  }

  private static async Task<IEnumerable<Option>> GetFieldTypeOptionsAsync(HttpContext httpContext)
  {
    return (await httpContext.GetStorage().GetRepository<int, FieldType, FieldTypeFilter>().GetAllAsync()).Select(
      ft => new Option(ft.Name, ft.Id.ToString())
    ).ToList();
  }
}