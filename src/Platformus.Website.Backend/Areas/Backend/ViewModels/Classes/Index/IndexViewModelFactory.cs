// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Primitives;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Classes;

public static class IndexViewModelFactory
{
  public static async Task<IndexViewModel> CreateAsync(HttpContext httpContext, string sorting, int offset, int limit, int total, IEnumerable<Class> classes)
  {
    return new IndexViewModel()
    {
      ClassOptions = await GetClassOptionsAsync(httpContext),
      Sorting = sorting,
      Offset = offset,
      Limit = limit,
      Total = total,
      Classes = classes.Select(ClassViewModelFactory.Create).ToList()
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