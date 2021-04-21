// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters
{
  public class DataTypeParameterFilter : IFilter
  {
    public int? Id { get; set; }
    public DataTypeFilter DataType { get; set; }
    public string Code { get; set; }
    public StringFilter Name { get; set; }

    public DataTypeParameterFilter() { }

    public DataTypeParameterFilter(int? id = null, DataTypeFilter dataType = null, string code = null, StringFilter name = null)
    {
      Id = id;
      DataType = dataType;
      Code = code;
      Name = name;
    }
  }
}