﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters
{
  public class DataTypeParameterValueFilter : IFilter
  {
    public int? Id { get; set; }
    public DataTypeParameterFilter DataTypeParameter { get; set; }
    public MemberFilter Member { get; set; }
    public StringFilter Value { get; set; }

    public DataTypeParameterValueFilter() { }

    public DataTypeParameterValueFilter(int? id = null, DataTypeParameterFilter dataTypeParameter = null, MemberFilter member = null, StringFilter value = null)
    {
      Id = id;
      DataTypeParameter = dataTypeParameter;
      Member = member;
      Value = value;
    }
  }
}