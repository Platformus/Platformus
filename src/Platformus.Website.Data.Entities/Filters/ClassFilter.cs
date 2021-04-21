// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters
{
  public class ClassFilter : IFilter
  {
    public int? Id { get; set; }
    public ClassFilter Parent { get; set; }
    public string Code { get; set; }
    public StringFilter Name { get; set; }
    public StringFilter PluralizedName { get; set; }
    public bool? IsAbstract { get; set; }

    public ClassFilter() { }

    public ClassFilter(int? id = null, ClassFilter parent = null, string code = null, StringFilter name = null, StringFilter pluralizedName = null, bool? isAbstract = null)
    {
      Id = id;
      Parent = parent;
      Code = code;
      Name = name;
      PluralizedName = pluralizedName;
      IsAbstract = isAbstract;
    }
  }
}