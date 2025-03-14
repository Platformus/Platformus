// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class ClassFilter : IFilter
{
  public IntegerFilter? Id { get; set; }
  public StringFilter? SingularName { get; set; }
  public StringFilter? PluralName { get; set; }
  public StringFilter? UrlSegment { get; set; }
  public StringFilter? CSharpName { get; set; }
  public EnumerableFilter<MemberFilter>? Members { get; set; }

  public ClassFilter() { }

  public ClassFilter(IntegerFilter? id = null, StringFilter? singularName = null, StringFilter? pluralName = null, StringFilter? urlSegment = null, StringFilter? cSharpName = null, EnumerableFilter<MemberFilter>? members = null)
  {
    this.Id = id;
    this.SingularName = singularName;
    this.PluralName = pluralName;
    this.UrlSegment = urlSegment;
    this.CSharpName = cSharpName;
    this.Members = members;
  }
}