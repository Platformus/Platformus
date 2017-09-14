// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Domain.Data.Entities
{
  /// <summary>
  /// Represents a class. The classes are used to describe the Platformus data model.
  /// </summary>
  public class Class : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the class.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the class identifier this class belongs to (as a parent class).
    /// </summary>
    public int? ClassId { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the class. It is set by the user and might be used for the class retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the class name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the pluralized class name.
    /// </summary>
    public string PluralizedName { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the class is abstract or not. Abstract classes can be used
    /// as parent ones for other classes. They can't be used to create objects.
    /// </summary>
    public bool IsAbstract { get; set; }

    public virtual Class Parent { get; set; }
    public virtual ICollection<Tab> Tabs { get; set; }
    public virtual ICollection<Member> Members { get; set; }
    public virtual ICollection<Object> Objects { get; set; }
  }
}