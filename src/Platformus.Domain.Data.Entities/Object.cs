// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Domain.Data.Entities
{
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