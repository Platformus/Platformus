// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class EndpointFilter : IFilter
{
  public int? Id { get; set; }
  public StringFilter Name { get; set; }
  public StringFilter UrlTemplate { get; set; }

  public EndpointFilter() { }

  public EndpointFilter(int? id = null, StringFilter name = null, StringFilter urlTemplate = null)
  {
    Id = id;
    Name = name;
    UrlTemplate = urlTemplate;
  }
}