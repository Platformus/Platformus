// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Primitives;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.DataTypes
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(HttpContext httpContext, string sorting, int offset, int limit, int total, IEnumerable<DataType> dataTypes)
    {
      return new IndexViewModel()
      {
        StorageDataTypeOptions = GetStorageDataTypeOptions(httpContext),
        Sorting = sorting,
        Offset = offset,
        Limit = limit,
        Total = total,
        DataTypes = dataTypes.Select(DataTypeViewModelFactory.Create)
      };
    }

    private static IEnumerable<Option> GetStorageDataTypeOptions(HttpContext httpContext)
    {
      IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();

      return new Option[]
      {
        new Option(localizer["All storage data types"], string.Empty),
        new Option(StorageDataTypes.Integer),
        new Option(StorageDataTypes.Decimal),
        new Option(StorageDataTypes.String),
        new Option(StorageDataTypes.DateTime)
      };
    }
  }
}