// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.DataTypeParameters
{
  public static class IndexViewModelFactory
  {
    public static async Task<IndexViewModel> CreateAsync(HttpContext httpContext, DataTypeParameterFilter filter, string sorting, int offset, int limit, int total, IEnumerable<DataTypeParameter> dataTypeParameters)
    {
      DataType dataType = await httpContext.GetStorage().GetRepository<int, DataType, DataTypeFilter>().GetByIdAsync((int)filter.DataType.Id);

      return new IndexViewModel()
      {
        Filter = filter,
        DataType = DataTypeViewModelFactory.Create(dataType),
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total,
        DataTypeParameters = dataTypeParameters.Select(DataTypeParameterViewModelFactory.Create)
      };
    }
  }
}