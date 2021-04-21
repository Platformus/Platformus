// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters
{
  public class DataSourceFilter : IFilter
  {
    public int? Id { get; set; }
    public EndpointFilter Endpoint { get; set; }
    public string Code { get; set; }

    public DataSourceFilter() { }

    public DataSourceFilter(int? id = null, EndpointFilter endpoint = null, string code = null)
    {
      Id = id;
      Endpoint = endpoint;
      Code = code;
    }
  }
}