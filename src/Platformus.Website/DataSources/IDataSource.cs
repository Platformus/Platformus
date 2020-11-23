// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Parameters;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.DataSources
{
  public interface IDataSource
  {
    IEnumerable<ParameterGroup> ParameterGroups { get; }
    string Description { get; }

    Task<dynamic> GetDataAsync(HttpContext httpContext, DataSource dataSource);
  }
}