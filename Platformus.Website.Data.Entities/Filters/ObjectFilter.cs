// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class ObjectFilter : IFilter
{
  public IntegerFilter? Id { get; set; }
  public ClassFilter? Class { get; set; }
  public EnumerableFilter<PropertyFilter>? Properties { get; set; }

  public ObjectFilter() { }

  public ObjectFilter(IntegerFilter? id = null, ClassFilter? @class = null, EnumerableFilter<PropertyFilter>? properties = null)
  {
    this.Id = id;
    this.Class = @class;
    this.Properties = properties;
  }
}