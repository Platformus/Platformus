// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Core.Domain.Models;

public class Variable : IModel<Data.Entities.Variable, VariableFilter>
{
  public int Id { get; set; }
  public Configuration? Configuration { get; set; }
  public string? Code { get; set; }
  public string? Name { get; set; }
  public string? Value { get; set; }
  public int? Position { get; set; }

  public Variable() { }

  public Variable(Data.Entities.Variable _variable) : this(_variable, mapConfiguration: true) { }

  public Variable(Data.Entities.Variable _variable, bool mapConfiguration = true)
  {
    this.Id = _variable.Id;
    this.Configuration = mapConfiguration ? _variable.Configuration == null ? new Configuration { Id = _variable.ConfigurationId } : new Configuration(_variable.Configuration, mapVariables: false) : null;
    this.Code = _variable.Code;
    this.Name = _variable.Name;
    this.Value = _variable.Value;
    this.Position = _variable.Position;
  }

  public Data.Entities.Variable ToEntity()
  {
    return new Data.Entities.Variable()
    {
      Id = this.Id,
      ConfigurationId = this.Configuration?.Id ?? 0,
      Code = this.Code,
      Name = this.Name,
      Value = this.Value,
      Position = this.Position
    };
  }
}