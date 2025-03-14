// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Domain.Models;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Models;

public class LocalizedProperty : IModel<Data.Entities.LocalizedProperty, LocalizedPropertyFilter>
{
  public Property? Property { get; set; }
  public Culture? Culture { get; set; }
  public string? StringValue { get; set; }
  
  public LocalizedProperty() { }

  public LocalizedProperty(Data.Entities.LocalizedProperty _localizedProperty) : this(_localizedProperty, mapProperty: true, mapCulture: true) { }

  public LocalizedProperty(Data.Entities.LocalizedProperty _localizedProperty, bool mapProperty = true, bool mapCulture = true)
  {
    this.Property = mapProperty ? _localizedProperty.Property == null ? new Property { Id = _localizedProperty.PropertyId } : new Property(_localizedProperty.Property, mapLocalizedProperties: false) : null;
    this.Culture = mapCulture ? _localizedProperty.Culture == null ? new Culture { Id = _localizedProperty.CultureId } : new Culture(_localizedProperty.Culture) : null;
    this.StringValue = _localizedProperty.StringValue;
  }

  public Data.Entities.LocalizedProperty ToEntity()
  {
    return new Data.Entities.LocalizedProperty
    {
      PropertyId = this.Property?.Id ?? 0,
      CultureId = this.Culture?.Id,
      StringValue = this.StringValue
    };
  }
}