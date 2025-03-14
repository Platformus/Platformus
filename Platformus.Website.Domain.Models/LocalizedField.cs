// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Domain.Models;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Models;

public class LocalizedField : IModel<Data.Entities.LocalizedField, LocalizedFieldFilter>
{
  public Field? Field { get; set; }
  public Culture? Culture { get; set; }
  public string? Name { get; set; }
  
  public LocalizedField() { }

  public LocalizedField(Data.Entities.LocalizedField _localizedField) : this(_localizedField, mapField: true, mapCulture: true) { }

  public LocalizedField(Data.Entities.LocalizedField _localizedField, bool mapField = true, bool mapCulture = true)
  {
    this.Field = mapField ? _localizedField.Field == null ? new Field { Id = _localizedField.FieldId } : new Field(_localizedField.Field, mapLocalizedFields: false) : null;
    this.Culture = mapCulture ? _localizedField.Culture == null ? new Culture { Id = _localizedField.CultureId } : new Culture(_localizedField.Culture) : null;
    this.Name = _localizedField.Name;
  }

  public Data.Entities.LocalizedField ToEntity()
  {
    return new Data.Entities.LocalizedField
    {
      FieldId = this.Field?.Id ?? 0,
      CultureId = this.Culture?.Id,
      Name = this.Name
    };
  }
}