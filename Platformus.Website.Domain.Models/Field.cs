// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Models;

public class Field : IModel<Data.Entities.Field, FieldFilter>
{
  public int Id { get; set; }
  public Form? Form { get; set; }
  public FieldType? FieldType { get; set; }
  public IEnumerable<LocalizedField>? LocalizedFields { get; set; }

  public Field() { }

  public Field(Data.Entities.Field _field) : this(_field, mapForm: true, mapFieldType: true, mapLocalizedFields: true) { }

  public Field(Data.Entities.Field _field, bool mapForm = true, bool mapFieldType = true, bool mapLocalizedFields = true)
  {
    this.Id = _field.Id;
    this.Form = mapForm ? _field.Form == null ? new Form { Id = _field.FormId } : new Form(_field.Form, mapFields: false) : null;
    this.FieldType = mapForm ? _field.FieldType == null ? new FieldType { Id = _field.FieldTypeId } : new FieldType(_field.FieldType) : null;
    this.LocalizedFields = mapLocalizedFields ? _field.LocalizedFields?.Select(lf => new LocalizedField(lf, mapField: false)).ToList() : null;
  }

  public Data.Entities.Field ToEntity()
  {
    return new Data.Entities.Field
    {
      Id = this.Id,
      FormId = this.Form?.Id ?? 0,
      FieldTypeId = this.FieldType?.Id,
      LocalizedFields = this.LocalizedFields?.Select(lf => lf.ToEntity()).ToList(),
    };
  }
}