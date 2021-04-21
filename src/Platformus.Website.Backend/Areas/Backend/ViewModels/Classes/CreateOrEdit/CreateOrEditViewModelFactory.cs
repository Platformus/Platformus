// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Classes
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public async Task<CreateOrEditViewModel> CreateAsync(HttpContext httpContext, Class @class)
    {
      if (@class == null)
        return new CreateOrEditViewModel()
        {
          ClassOptions = await this.GetClassOptionsAsync(httpContext),
        };

      return new CreateOrEditViewModel()
      {
        Id = @class.Id,
        ClassId = @class.ClassId,
        ClassOptions = await this.GetClassOptionsAsync(httpContext),
        Code = @class.Code,
        Name = @class.Name,
        PluralizedName = @class.PluralizedName,
        IsAbstract = @class.IsAbstract
      };
    }

    private async Task<IEnumerable<Option>> GetClassOptionsAsync(HttpContext httpContext)
    {
      IStringLocalizer<CreateOrEditViewModelFactory> localizer = httpContext.GetStringLocalizer<CreateOrEditViewModelFactory>();
      List<Option> options = new List<Option>();

      options.Add(new Option(localizer["Parent class not specified"], string.Empty));
      options.AddRange(
        (await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetAllAsync(new ClassFilter(isAbstract: true))).Select(
          c => new Option(c.Name, c.Id.ToString())
        )
      );

      return options;
    }
  }
}