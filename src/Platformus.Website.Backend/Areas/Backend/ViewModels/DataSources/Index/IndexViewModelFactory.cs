// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.DataSources
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(HttpContext httpContext, DataSourceFilter filter, IEnumerable<DataSource> dataSources, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();

      return new IndexViewModel()
      {
        Filter = filter,
        Grid = new GridViewModelFactory().Create(
          httpContext,
          new FilterViewModelFactory().Create(httpContext, "Code.Contains", localizer["Code"]),
          orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["Code"], "Code"),
            new GridColumnViewModelFactory().Create(localizer["Data provider C# class name"], "DataProviderCSharpClassName"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          dataSources.Select(ds => new DataSourceViewModelFactory().Create(ds)),
          "_DataSource"
        )
      };
    }
  }
}