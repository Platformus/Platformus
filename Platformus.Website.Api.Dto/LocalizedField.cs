// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;
using Platformus.Core.Api.Dto;

namespace Platformus.Website.Api.Dto;

[Magicalized("/api/v1/localized-fields")]
[AuthenticatedOnly]
public class LocalizedField : IDto<Domain.Models.LocalizedField>
{
  public Field? Field { get; set; }
  public Culture? Culture { get; set; }
  public string? Name { get; set; }

  public LocalizedField() { }

  public LocalizedField(Domain.Models.LocalizedField _localizedField)
  {
    this.Field = _localizedField.Field == null ? null : new Field(_localizedField.Field);
    this.Culture = _localizedField.Culture == null ? null : new Culture(_localizedField.Culture);
    this.Name = _localizedField.Name;
  }

  public Domain.Models.LocalizedField ToModel()
  {
    return new Domain.Models.LocalizedField()
    {
      Field = this.Field?.ToModel(),
      Culture = this.Culture?.ToModel(),
      Name = this.Name
    };
  }
}