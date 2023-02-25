// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class MemberFilter : IFilter
{
  public int? Id { get; set; }
  public ClassFilter Class { get; set; }
  public TabFilter Tab { get; set; }
  public string Code { get; set; }
  public StringFilter Name { get; set; }
  public DataTypeFilter PropertyDataType { get; set; }
  public bool? IsPropertyVisibleInList { get; set; }
  public ClassFilter RelationClass { get; set; }
  public bool? IsRelationSingleParent { get; set; }

  public MemberFilter() { }

  public MemberFilter(int? id = null, ClassFilter @class = null, TabFilter tab = null, string code = null, StringFilter name = null, DataTypeFilter propertyDataType = null, bool? isPropertyVisibleInList = null, ClassFilter relationClass = null, bool? isRelationSingleParent = null)
  {
    Id = id;
    Class = @class;
    Tab = tab;
    Code = code;
    Name = name;
    PropertyDataType = propertyDataType;
    IsPropertyVisibleInList = isPropertyVisibleInList;
    RelationClass = relationClass;
    IsRelationSingleParent = isRelationSingleParent;
  }
}