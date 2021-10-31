// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Members
{
  public static class IndexViewModelFactory
  {
    public static async Task<IndexViewModel> CreateAsync(HttpContext httpContext, MemberFilter filter, string sorting, int offset, int limit, int total, IEnumerable<Member> members)
    {
      Class @class = await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetByIdAsync((int)filter.Class.Id);

      return new IndexViewModel()
      {
        Filter = filter,
        Class = ClassViewModelFactory.Create(@class),
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total,
        Members = members.Select(MemberViewModelFactory.Create)
      };
    }
  }
}