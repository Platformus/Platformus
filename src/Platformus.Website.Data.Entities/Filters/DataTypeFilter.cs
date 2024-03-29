﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class DataTypeFilter : IFilter
{
  public int? Id { get; set; }
  public string StorageDataType { get; set; }
  public StringFilter Name { get; set; }

  public DataTypeFilter() { }

  public DataTypeFilter(int? id = null, string storageDataType = null, StringFilter name = null)
  {
    Id = id;
    StorageDataType = storageDataType;
    Name = name;
  }
}