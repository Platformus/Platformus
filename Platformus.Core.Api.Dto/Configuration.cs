// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Core.Api.Dto;

[Magicalized("/api/v1/configurations")]
[AuthenticatedOnly]
public class Configuration : IDto<Domain.Models.Configuration>
{
  public int Id { get; set; }
  public string? Code { get; set; }
  public string? Name { get; set; }
  public int? Position { get; set; }
  public IEnumerable<Variable>? Variables { get; set; }

  public Configuration() { }

  public Configuration(Domain.Models.Configuration _configuration)
  {
    this.Id = _configuration.Id;
    this.Code = _configuration.Code;
    this.Name = _configuration.Name;
    this.Position = _configuration.Position;
    this.Variables = _configuration.Variables?.Select(v => new Variable(v)).ToList();
  }

  public Domain.Models.Configuration ToModel()
  {
    return new Domain.Models.Configuration()
    {
      Id = this.Id,
      Code = this.Code,
      Name = this.Name,
      Position = this.Position
    };
  }
}