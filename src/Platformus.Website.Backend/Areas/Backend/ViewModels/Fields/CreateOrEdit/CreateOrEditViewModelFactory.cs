// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Fields
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, Field field)
    {
      if (field == null)
        return new CreateOrEditViewModel()
        {
          FieldTypeOptions = await this.GetFieldTypeOptionsAsync(httpContext),
          NameLocalizations = this.GetLocalizations(httpContext)
        };

      return new CreateOrEditViewModel()
      {
        Id = field.Id,
        FieldTypeId = field.FieldTypeId,
        FieldTypeOptions = await this.GetFieldTypeOptionsAsync(httpContext),
        Code = field.Code,
        NameLocalizations = this.GetLocalizations(httpContext, field.Name),
        IsRequired = field.IsRequired,
        MaxLength = field.MaxLength,
        Position = field.Position
      };
    }

    private async Task<IEnumerable<Option>> GetFieldTypeOptionsAsync(HttpContext httpContext)
    {
      return (await httpContext.GetStorage().GetRepository<int, FieldType, FieldTypeFilter>().GetAllAsync()).Select(
        ft => new Option(ft.Name, ft.Id.ToString())
      );
    }
  }
}