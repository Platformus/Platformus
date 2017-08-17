// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Domain.Data.Entities
{
  /// <summary>
  /// Represents an object. The object is the central unit in the Platformus data model.
  /// They are described by the classes and members and store the data using the properties.
  /// </summary>
  public class Object : IEntity
  {
    public int Id { get; set; }
    public int ClassId { get; set; }

    public virtual Class Class { get; set; }
    public virtual ICollection<Property> Properties { get; set; }
    public virtual ICollection<Relation> PrimaryRelations { get; set; }
    public virtual ICollection<Relation> ForeignRelations { get; set; }
  }
}