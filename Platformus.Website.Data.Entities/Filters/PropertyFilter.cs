// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class PropertyFilter : IFilter
{
  public IntegerFilter? Id { get; set; }
  public ClassFilter? Class { get; set; }
  public MemberFilter? Member { get; set; }
  public DecimalFilter? DecimalValue { get; set; }
  public StringFilter? StringValue { get; set; }
  public EnumerableFilter<LocalizedPropertyFilter>? LocalizedProperties { get; set; }

  public PropertyFilter() { }

  public PropertyFilter(IntegerFilter? id = null, ClassFilter? @class = null, MemberFilter? member = null, DecimalFilter? decimalValue = null, StringFilter? stringValue = null, EnumerableFilter<LocalizedPropertyFilter>? localizedProperties = null)
  {
    this.Id = id;
    this.Class = @class;
    this.Member = member;
    this.DecimalValue = decimalValue;
    this.StringValue = stringValue;
    this.LocalizedProperties = localizedProperties;
  }
}