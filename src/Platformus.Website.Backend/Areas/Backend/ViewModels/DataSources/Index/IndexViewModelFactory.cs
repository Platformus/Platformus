// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.DataSources;

public static class IndexViewModelFactory
{
  public static async Task<IndexViewModel> CreateAsync(HttpContext httpContext, DataSourceFilter filter, string sorting, int offset, int limit, int total, IEnumerable<DataSource> dataSources)
  {
    Data.Entities.Endpoint endpoint = await httpContext.GetStorage().GetRepository<int, Data.Entities.Endpoint, EndpointFilter>().GetByIdAsync((int)filter.Endpoint.Id);

    return new IndexViewModel()
    {
      Filter = filter,
      Endpoint = EndpointViewModelFactory.Create(endpoint),
      Sorting = sorting,
      Offset = offset,
      Limit = limit,
      Total = total,
      DataSources = dataSources.Select(DataSourceViewModelFactory.Create).ToList()
    };
  }
}