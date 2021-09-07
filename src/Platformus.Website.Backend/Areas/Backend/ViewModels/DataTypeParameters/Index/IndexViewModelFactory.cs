// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.DataTypeParameters
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(DataTypeParameterFilter filter, string sorting, int offset, int limit, int total, IEnumerable<DataTypeParameter> dataTypeParameters)
    {
      return new IndexViewModel()
      {
        Filter = filter,
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total,
        DataTypeParameters = dataTypeParameters.Select(DataTypeParameterViewModelFactory.Create)
      };
    }
  }
}