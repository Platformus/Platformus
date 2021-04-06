// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Core.Primitives;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.DataTypes
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(HttpContext httpContext, DataTypeFilter filter, IEnumerable<DataType> dataTypes, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory().Create(
          httpContext,
          new[] {
            new FilterViewModelFactory().Create(httpContext, "StorageDataType", localizer["Storage data type"], this.GetStorageDataTypeOptions(httpContext)),
            new FilterViewModelFactory().Create(httpContext, "Name.Contains", localizer["Name"])
          },
          orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["Storage data type"], "StorageDataType"),
            new GridColumnViewModelFactory().Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory().Create(localizer["Data type parameters"]),
            new GridColumnViewModelFactory().Create(localizer["Position"], "Position"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          dataTypes.Select(dt => new DataTypeViewModelFactory().Create(dt)),
          "_DataType"
        )
      };
    }

    private IEnumerable<Option> GetStorageDataTypeOptions(HttpContext httpContext)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();

      return new Option[]
      {
        new Option(localizer["All storage data types"], string.Empty),
        new Option(StorageDataType.Integer),
        new Option(StorageDataType.Decimal),
        new Option(StorageDataType.String),
        new Option(StorageDataType.DateTime)
      };
    }
  }
}