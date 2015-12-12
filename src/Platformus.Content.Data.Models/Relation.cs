// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Content.Data.Models
{
  public class Relation : IEntity
  {
    //[Key]
    //[Required]
    public int Id { get; set; }

    //[Required]
    public int MemberId { get; set; }

    //[Required]
    public int PrimaryId { get; set; }

    //[Required]
    public int ForeignId { get; set; }

    public virtual Member Member { get; set; }
    public virtual Object Primary { get; set; }
    public virtual Object Foreign { get; set; }
  }
}