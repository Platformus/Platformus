// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters;

public class VariableFilter : IFilter
{
  public IntegerFilter? Id { get; set; }
  public ConfigurationFilter? Configuration { get; set; }
  public StringFilter? Code { get; set; }
  public StringFilter? Name { get; set; }
  public StringFilter? Value { get; set; }

  public VariableFilter() { }

  public VariableFilter(IntegerFilter? id = null, ConfigurationFilter? configuration = null, StringFilter? code = null, StringFilter? name = null, StringFilter? value = null)
  {
    this.Id = id;
    this.Configuration = configuration;
    this.Code = code;
    this.Name = name;
    this.Value = value;
  }
}