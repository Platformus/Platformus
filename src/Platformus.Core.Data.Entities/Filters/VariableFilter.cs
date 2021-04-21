// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters
{
  public class VariableFilter : IFilter
  {
    public int? Id { get; set; }
    public ConfigurationFilter Configuration { get; set; }
    public string Code { get; set; }
    public StringFilter Name { get; set; }
    public StringFilter Value { get; set; }

    public VariableFilter() { }

    public VariableFilter(int? id = null, ConfigurationFilter configuration = null, string code = null, StringFilter name = null, StringFilter value = null)
    {
      Id = id;
      Configuration = configuration;
      Code = code;
      Name = name;
      Value = value;
    }
  }
}