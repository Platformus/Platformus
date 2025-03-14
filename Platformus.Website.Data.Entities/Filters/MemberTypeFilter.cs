// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class MemberTypeFilter : IFilter
{
  public StringFilter? Id { get; set; }
  public StringFilter? Name { get; set; }
  public EnumerableFilter<MemberFilter>? Members { get; set; }

  public MemberTypeFilter() { }

  public MemberTypeFilter(StringFilter? id = null, StringFilter? name = null, EnumerableFilter<MemberFilter>? members = null)
  {
    this.Id = id;
    this.Name = name;
    this.Members = members;
  }
}