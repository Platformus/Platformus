// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters;

public class ConfigurationFilter : IFilter
{
  public IntegerFilter? Id { get; set; }
  public StringFilter? Code { get; set; }
  public StringFilter? Name { get; set; }
  public EnumerableFilter<VariableFilter>? Variables { get; set; }

  public ConfigurationFilter() { }

  public ConfigurationFilter(IntegerFilter? id = null, StringFilter? code = null, StringFilter? name = null, EnumerableFilter<VariableFilter>? variables = null)
  {
    this.Id = id;
    this.Code = code;
    this.Name = name;
    this.Variables = variables;
  }
}