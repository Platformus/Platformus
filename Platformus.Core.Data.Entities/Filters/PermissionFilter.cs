// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters;

public class PermissionFilter : IFilter
{
  public IntegerFilter? Id { get; set; }
  public StringFilter? Code { get; set; }
  public StringFilter? Name { get; set; }

  public PermissionFilter() { }

  public PermissionFilter(IntegerFilter? id = null, StringFilter? code = null, StringFilter? name = null)
  {
    this.Id = id;
    this.Code = code;
    this.Name = name;
  }
}