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

namespace Platformus.Website.Backend.ViewModels.FileManager
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModel Create(HttpContext httpContext, FileFilter filter, IEnumerable<File> files, string orderBy, int skip, int take, int total)
    {
      IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory().Create(
          httpContext,
          new FilterViewModelFactory().Create(httpContext, "Name.Contains", localizer["Name"]),
          orderBy, skip, take, total,
          new[] {
            new GridColumnViewModelFactory().Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory().Create(localizer["Size"], "Size"),
            new GridColumnViewModelFactory().CreateEmpty()
          },
          files.Select(f => new FileViewModelFactory().Create(f)),
          "_File"
        )
      };
    }
  }
}