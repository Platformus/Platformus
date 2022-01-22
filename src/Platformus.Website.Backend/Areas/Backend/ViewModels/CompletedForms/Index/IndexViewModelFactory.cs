// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.CompletedForms
{
  public static class IndexViewModelFactory
  {
    public static async Task<IndexViewModel> CreateAsync(HttpContext httpContext, CompletedFormFilter filter, string sorting, int offset, int limit, int total, IEnumerable<CompletedForm> completedForms)
    {
      Form form = await httpContext.GetStorage().GetRepository<int, Form, FormFilter>().GetByIdAsync(
        (int)filter.Form.Id,
        new Inclusion<Form>(f => f.Name.Localizations)
      );

      return new IndexViewModel()
      {
        Filter = filter,
        Form = FormViewModelFactory.Create(form),
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total,
        CompletedForms = completedForms.Select(CompletedFormViewModelFactory.Create).ToList()
      };
    }
  }
}