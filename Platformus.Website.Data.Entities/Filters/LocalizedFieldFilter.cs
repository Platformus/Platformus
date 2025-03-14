// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Website.Filters;

public class LocalizedFieldFilter : IFilter
{
  public FieldFilter? Field { get; set; }
  public CultureFilter? Culture { get; set; }
  public StringFilter? Name { get; set; }

  public LocalizedFieldFilter() { }

  public LocalizedFieldFilter(FieldFilter? field = null, CultureFilter? culture = null, StringFilter? name = null)
  {
    this.Field = field;
    this.Culture = culture;
    this.Name = name;
  }
}