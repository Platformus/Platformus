// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Website.Filters;

public class PropertyFilter : IFilter
{
  public int? Id { get; set; }
  public ObjectFilter Object { get; set; }
  public MemberFilter Member { get; set; }
  public IntegerFilter IntegerValue { get; set; }
  public DecimalFilter DecimalValue { get; set; }

  [FilterShortcut("StringValue.Localizations[]")]
  public LocalizationFilter StringValue { get; set; }
  public DateTimeFilter DateTimeValue { get; set; }

  public PropertyFilter() { }

  public PropertyFilter(int? id = null, ObjectFilter @object = null, MemberFilter member = null, IntegerFilter integerValue = null, DecimalFilter decimalValue = null, LocalizationFilter stringValue = null, DateTimeFilter dateTimeValue = null)
  {
    Id = id;
    Object = @object;
    Member = member;
    IntegerValue = integerValue;
    DecimalValue = decimalValue;
    StringValue = stringValue;
    DateTimeValue = dateTimeValue;
  }
}