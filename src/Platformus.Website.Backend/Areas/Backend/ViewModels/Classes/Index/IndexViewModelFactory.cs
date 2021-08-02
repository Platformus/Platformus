// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Classes
{
  public static class IndexViewModelFactory
  {
    public static async Task<IndexViewModel> CreateAsync(HttpContext httpContext, ClassFilter filter, IEnumerable<Class> classes, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();

      return new IndexViewModel()
      {
        Grid = GridViewModelFactory.Create(
          httpContext,
          new[] {
            FilterViewModelFactory.Create(httpContext, "Parent.Id", localizer["Parent class"], await GetClassOptionsAsync(httpContext)),
            FilterViewModelFactory.Create(httpContext, "Name.Contains", localizer["Name"])
          },
          orderBy, skip, take, total,
          new[] {
            GridColumnViewModelFactory.Create(localizer["Parent class"]),
            GridColumnViewModelFactory.Create(localizer["Name"], "Name"),
            GridColumnViewModelFactory.Create(localizer["Is abstract"], "IsAbstract"),
            GridColumnViewModelFactory.Create(localizer["Tabs"]),
            GridColumnViewModelFactory.Create(localizer["Members"]),
            GridColumnViewModelFactory.CreateEmpty()
          },
          classes.Select(ClassViewModelFactory.Create),
          "_Class"
        )
      };
    }

    private static async Task<IEnumerable<Option>> GetClassOptionsAsync(HttpContext httpContext)
    {
      IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();
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