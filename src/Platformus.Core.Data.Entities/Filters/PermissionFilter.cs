﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters;

public class PermissionFilter : IFilter
{
  public int? Id { get; set; }
  public string Code { get; set; }
  public StringFilter Name { get; set; }

  public PermissionFilter() { }

  public PermissionFilter(int? id = null, string code = null, StringFilter name = null)
  {
    Id = id;
    Code = code;
    Name = name;
  }
}