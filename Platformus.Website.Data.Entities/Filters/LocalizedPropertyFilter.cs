// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Website.Filters;

public class LocalizedPropertyFilter : IFilter
{
  public PropertyFilter? Property { get; set; }
  public CultureFilter? Culture { get; set; }
  public StringFilter? StringValue { get; set; }

  public LocalizedPropertyFilter() { }

  public LocalizedPropertyFilter(PropertyFilter? property = null, CultureFilter? culture = null, StringFilter? stringValue = null)
  {
    this.Property = property;
    this.Culture = culture;
    this.StringValue = stringValue;
  }
}