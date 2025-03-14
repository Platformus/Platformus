// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class MemberFilter : IFilter
{
  public IntegerFilter? Id { get; set; }
  public ClassFilter? Class { get; set; }
  public MemberTypeFilter? MemberType { get; set; }
  public StringFilter? Name { get; set; }
  public StringFilter? CSharpName { get; set; }
  public BooleanFilter? IsLocalizable { get; set; }
  public BooleanFilter? IsShownInList { get; set; }

  public MemberFilter() { }

  public MemberFilter(IntegerFilter? id = null, ClassFilter? @class = null, MemberTypeFilter? memberType = null, StringFilter? name = null, StringFilter? cSharpName = null, BooleanFilter? isLocalizable = null, BooleanFilter? isShownInList = null)
  {
    this.Id = id;
    this.Class = @class;
    this.MemberType = memberType;
    this.Name = name;
    this.CSharpName = cSharpName;
    this.IsLocalizable = isLocalizable;
    this.IsShownInList = isShownInList;
  }
}