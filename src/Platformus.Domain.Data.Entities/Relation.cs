// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Domain.Data.Entities
{
  /// <summary>
  /// Represents a relation between two objects. The relations are used to connect primary and foreign objects and
  /// the corresponding class member.
  /// </summary>
  public class Relation : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the relation.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the member identifier this relation is related to.
    /// </summary>
    public int MemberId { get; set; }

    /// <summary>
    /// Gets or sets the primary object identifier this relation belongs to.
    /// </summary>
    public int PrimaryId { get; set; }

    /// <summary>
    /// Gets or sets the foreign object identifier this relation belongs to.
    /// </summary>
    public int ForeignId { get; set; }

    public virtual Member Member { get; set; }
    public virtual Object Primary { get; set; }
    public virtual Object Foreign { get; set; }
  }
}