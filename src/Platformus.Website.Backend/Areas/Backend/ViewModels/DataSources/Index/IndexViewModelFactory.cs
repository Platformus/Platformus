// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.DataSources
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(DataSourceFilter filter, string sorting, int offset, int limit, int total, IEnumerable<DataSource> dataSources)
    {
      return new IndexViewModel()
      {
        Filter = filter,
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total,
        DataSources = dataSources.Select(DataSourceViewModelFactory.Create)
      };
    }
  }
}