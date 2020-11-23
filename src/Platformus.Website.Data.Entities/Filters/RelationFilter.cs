// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters
{
  public class RelationFilter : IFilter
  {
    public int? Id { get; set; }
    public MemberFilter Member { get; set; }
    public ObjectFilter Primary { get; set; }
    public ObjectFilter Foreign { get; set; }
  }
}