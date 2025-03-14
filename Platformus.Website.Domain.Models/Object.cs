// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Models;

public class Object : IModel<Data.Entities.Object, ObjectFilter>
{
  public int Id { get; set; }
  public Class? Class { get; set; }
  public IEnumerable<Property>? Properties { get; set; }

  public Object() { }

  public Object(Data.Entities.Object _object) : this(_object, mapClass: true, mapProperties: true) { }

  public Object(Data.Entities.Object _object, bool mapClass = true, bool mapProperties = true)
  {
    this.Id = _object.Id;
    this.Class = mapClass ? _object.Class == null ? new Class { Id = _object.ClassId } : new Class(_object.Class, mapObjects: false) : null;
    this.Properties = mapProperties ? _object.Properties?.Select(p => new Property(p, mapObject: false)).ToList() : null;
  }

  public Data.Entities.Object ToEntity()
  {
    return new Data.Entities.Object()
    {
      Id = this.Id,
      ClassId = this.Class?.Id ?? 0,
      Properties = this.Properties?.Select(p => p.ToEntity()).ToList(),
    };
  }
}