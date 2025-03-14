// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Website.Api.Dto;

[Magicalized("/api/v1/field-types", Magicalizer.Api.Dto.Abstractions.HttpMethod.Get)]
[AuthenticatedOnly]
public class FieldType : IDto<Domain.Models.FieldType>
{
  public string? Id { get; set; }
  public string? Name { get; set; }

  public FieldType() { }

  public FieldType(Domain.Models.FieldType _fieldType)
  {
    this.Id = _fieldType.Id;
    this.Name = _fieldType.Name;
  }

  public Domain.Models.FieldType ToModel()
  {
    return new Domain.Models.FieldType()
    {
      Id = this.Id,
      Name = this.Name
    };
  }
}