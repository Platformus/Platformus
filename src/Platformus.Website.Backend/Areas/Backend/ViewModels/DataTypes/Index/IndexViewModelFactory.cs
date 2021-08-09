// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.DataTypes
{
  public static class IndexViewModelFactory
  {
    public static IndexViewModel Create(HttpContext httpContext, DataTypeFilter filter, IEnumerable<DataType> dataTypes, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();

      return new IndexViewModel()
      {
        Grid = GridViewModelFactory.Create(
          httpContext,
          new[] {
            FilterViewModelFactory.Create(httpContext, "StorageDataType", localizer["Storage data type"], GetStorageDataTypeOptions(httpContext)),
            FilterViewModelFactory.Create(httpContext, "Name.Contains", localizer["Name"])
          },
          orderBy, skip, take, total,
          new[] {
            GridColumnViewModelFactory.Create(localizer["Storage data type"], "StorageDataType"),
            GridColumnViewModelFactory.Create(localizer["Name"], "Name"),
            GridColumnViewModelFactory.Create(localizer["Data type parameters"]),
            GridColumnViewModelFactory.Create(localizer["Position"], "Position"),
            GridColumnViewModelFactory.CreateEmpty()
          },
          dataTypes.Select(DataTypeViewModelFactory.Create),
          "_DataType"
        )
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