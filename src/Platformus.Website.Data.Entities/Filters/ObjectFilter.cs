// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Website.Filters;

public class ObjectFilter : IFilter
{
  public int? Id { get; set; }
  public ClassFilter Class { get; set; }

  [FilterShortcut("Properties[]")]
  public PropertyFilter Property { get; set; }

  [FilterShortcut("Properties[].StringValue.Localizations[]")]
  public LocalizationFilter StringValue { get; set; }

  [FilterShortcut("ForeignRelations[].Primary")]
  public ObjectFilter Primary { get; set; }

  [FilterShortcut("PrimaryRelations[].Foreign")]
  public ObjectFilter Foreign { get; set; }

  public ObjectFilter() { }

  public ObjectFilter(int? id = null, ClassFilter @class = null, PropertyFilter property = null, LocalizationFilter stringValue = null, ObjectFilter primary = null, ObjectFilter foreign = null)
  {
    Id = id;
    Class = @class;
    Property = property;
    StringValue = stringValue;
    Primary = primary;
    Foreign = foreign;
  }
}