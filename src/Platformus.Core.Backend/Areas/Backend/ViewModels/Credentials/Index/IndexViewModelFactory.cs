// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.ViewModels.Credentials
{
  public static class IndexViewModelFactory
  {
    public static async Task<IndexViewModel> CreateAsync(HttpContext httpContext, CredentialFilter filter, string sorting, int offset, int limit, int total, IEnumerable<Credential> credentials)
    {
      User user = await httpContext.GetStorage().GetRepository<int, User, UserFilter>().GetByIdAsync((int)filter.User.Id);

      return new IndexViewModel()
      {
        User = UserViewModelFactory.Create(user),
        Filter = filter,
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total,
        Credentials = credentials.Select(CredentialViewModelFactory.Create)
      };
    }
  }
}