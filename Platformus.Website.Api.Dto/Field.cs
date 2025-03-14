// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Website.Api.Dto;

[Magicalized("/api/v1/fields")]
[AuthenticatedOnly]
public class Field : IDto<Domain.Models.Field>
{
  public int Id { get; set; }
  public Form? Form { get; set; }
  public FieldType? FieldType { get; set; }
  public IEnumerable<LocalizedField>? LocalizedFields { get; set; }

  public Field() { }

  public Field(Domain.Models.Field _field)
  {
    this.Id = _field.Id;
    this.Form = _field.Form == null ? null : new Form(_field.Form);
    this.FieldType = _field.FieldType == null ? null : new FieldType(_field.FieldType);
    this.LocalizedFields = _field.LocalizedFields?.Select(lf => new LocalizedField(lf)).ToList();
  }

  public Domain.Models.Field ToModel()
  {
    return new Domain.Models.Field()
    {
      Id = this.Id,
      Form = this.Form?.ToModel(),
      FieldType = this.FieldType?.ToModel(),
      LocalizedFields = this.LocalizedFields?.Select(lf => lf.ToModel()),
    };
  }

  public LocalizedField Localized()
  {
    return this.Localized(CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
  }

  public LocalizedField Localized(string cultureId)
  {
    return this.LocalizedFields?.FirstOrDefault(lf => lf.Culture!.Id == cultureId) ?? new LocalizedField();
  }
}