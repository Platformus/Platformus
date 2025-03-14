// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;
using Platformus.Core.Api.Dto;

namespace Platformus.Website.Api.Dto;

[Magicalized("/api/v1/localized-properties")]
[AuthenticatedOnly]
public class LocalizedProperty : IDto<Domain.Models.LocalizedProperty>
{
  public Property? Property { get; set; }
  public Culture? Culture { get; set; }
  public string? StringValue { get; set; }

  public LocalizedProperty() { }

  public LocalizedProperty(Domain.Models.LocalizedProperty _localizedProperty)
  {
    this.Property = _localizedProperty.Property == null ? null : new Property(_localizedProperty.Property);
    this.Culture = _localizedProperty.Culture == null ? null : new Culture(_localizedProperty.Culture);
    this.StringValue = _localizedProperty.StringValue;
  }

  public Domain.Models.LocalizedProperty ToModel()
  {
    return new Domain.Models.LocalizedProperty()
    {
      Property = this.Property?.ToModel(),
      Culture = this.Culture?.ToModel(),
      StringValue = this.StringValue
    };
  }
}