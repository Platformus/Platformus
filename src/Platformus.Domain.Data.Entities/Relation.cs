// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Domain.Data.Entities
{
  /// <summary>
  /// Represents a relation between two objects. The relations are used to connect primary and foreign objects and
  /// the corresponding member.
  /// </summary>
  public class Relation : IEntity
  {
    public int Id { get; set; }
    public int MemberId { get; set; }
    public int PrimaryId { get; set; }
    public int ForeignId { get; set; }

    public virtual Member Member { get; set; }
    public virtual Object Primary { get; set; }
    public virtual Object Foreign { get; set; }
  }
}