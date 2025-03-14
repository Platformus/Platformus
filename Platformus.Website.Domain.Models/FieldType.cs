// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Models;

public class FieldType : IModel<Data.Entities.FieldType, FieldTypeFilter>
{
  public string? Id { get; set; }
  public string? Name { get; set; }

  public FieldType() { }

  public FieldType(Data.Entities.FieldType _fieldType)
  {
    this.Id = _fieldType.Id;
    this.Name = _fieldType.Name;
  }

  public Data.Entities.FieldType ToEntity()
  {
    return new Data.Entities.FieldType
    {
      Id = this.Id,
      Name = this.Name
    };
  }
}