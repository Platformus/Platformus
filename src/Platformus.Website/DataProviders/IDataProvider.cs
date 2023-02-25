// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Core;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.DataProviders;

/// <summary>
/// Describes a data provider. Data sources use data providers selected by the users to build models.
/// Data provider optionally takes any information from the HTTP(S) request (URL, cookies etc.), from the external services,
/// some hardcoded data, and builds a dynamic model object.
/// </summary>
public interface IDataProvider : IParameterized
{
  /// <summary>
  /// Returns a dynamic model object.
  /// </summary>
  /// <param name="httpContext">Current <see cref="HttpContext"/> to get the required services or information from.</param>
  /// <param name="dataSource">A data source that uses the data provider.</param>
  Task<dynamic> GetDataAsync(HttpContext httpContext, DataSource dataSource);
}