// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Core.Api.Dto;

[Magicalized("/api/v1/variables")]
[AuthenticatedOnly]
public class Variable : IDto<Domain.Models.Variable>
{
  public int Id { get; set; }
  public Configuration? Configuration { get; set; }
  public string? Code { get; set; }
  public string? Name { get; set; }
  public string? Value { get; set; }
  public int? Position { get; set; }

  public Variable() { }

  public Variable(Domain.Models.Variable _variable)
  {
    this.Id = _variable.Id;
    this.Configuration = _variable.Configuration == null ? null : new Configuration(_variable.Configuration);
    this.Code = _variable.Code;
    this.Name = _variable.Name;
    this.Value = _variable.Value;
    this.Position = _variable.Position;
  }

  public Domain.Models.Variable ToModel()
  {
    return new Domain.Models.Variable()
    {
      Id = this.Id,
      Configuration = this.Configuration?.ToModel(),
      Code = this.Code,
      Name = this.Name,
      Value = this.Value,
      Position = this.Position
    };
  }
}