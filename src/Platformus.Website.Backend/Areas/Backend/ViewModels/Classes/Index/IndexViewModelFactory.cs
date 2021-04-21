// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Classes
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public async Task<IndexViewModel> CreateAsync(HttpContext httpContext, ClassFilter filter, IEnumerable<Class> classes, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory().Create(
          httpContext,
          new[] {
            new FilterViewModelFactory().Create(httpContext, "Parent.Id", localizer["Parent class"], await this.GetClassOptionsAsync(httpContext)),
            new FilterViewModelFactory().Create(httpContext, "Name.Contains", localizer["Name"])
          },
          orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["Parent class"]),
            new GridColumnViewModelFactory().Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory().Create(localizer["Is abstract"], "IsAbstract"),
            new GridColumnViewModelFactory().Create(localizer["Tabs"]),
            new GridColumnViewModelFactory().Create(localizer["Members"]),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          classes.Select(c => new ClassViewModelFactory().Create(c)),
          "_Class"
        )
      };
    }

    private async Task<IEnumerable<Option>> GetClassOptionsAsync(HttpContext httpContext)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();
      List<Option> options = new List<Option>();

      options.Add(new Option(localizer["All parent classes"], string.Empty));
      options.AddRange(
        (await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetAllAsync(new ClassFilter(isAbstract: true))).Select(
          c => new Option(c.Name, c.Id.ToString())
        )
      );

      return options;
    }
  }
}