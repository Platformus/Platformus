// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class TabFilter : IFilter
{
  public int? Id { get; set; }
  public ClassFilter Class { get; set; }
  public StringFilter Name { get; set; }

  public TabFilter() { }

  public TabFilter(int? id = null, ClassFilter @class = null, StringFilter name = null)
  {
    Id = id;
    Class = @class;
    Name = name;
  }
}