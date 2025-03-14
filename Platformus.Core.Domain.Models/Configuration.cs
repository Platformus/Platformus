// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Core.Domain.Models;

public class Configuration : IModel<Data.Entities.Configuration, ConfigurationFilter>
{
  public int Id { get; set; }
  public string? Code { get; set; }
  public string? Name { get; set; }
  public int? Position { get; set; }
  public IEnumerable<Variable>? Variables { get; set; }

  public Configuration() { }

  public Configuration(Data.Entities.Configuration _configuration) : this(_configuration, mapVariables: true) { }

  public Configuration(Data.Entities.Configuration _configuration, bool mapVariables = true)
  {
    this.Id = _configuration.Id;
    this.Code = _configuration.Code;
    this.Name = _configuration.Name;
    this.Position = _configuration.Position;
    this.Variables = mapVariables ? _configuration.Variables?.Select(v => new Variable(v, mapConfiguration: false)).ToList() : null;
  }

  public Data.Entities.Configuration ToEntity()
  {
    return new Data.Entities.Configuration()
    {
      Id = this.Id,
      Code = this.Code,
      Name = this.Name,
      Position = this.Position
    };
  }
}