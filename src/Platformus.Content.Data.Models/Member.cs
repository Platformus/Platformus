// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Content.Data.Models
{
  public class Member : IEntity
  {
    //[Key]
    //[Required]
    public int Id { get; set; }

    //[Required]
    public int ClassId { get; set; }
    public int? TabId { get; set; }

    //[Required]
    //[StringLength(32)]
    public string Code { get; set; }

    //[Required]
    //[StringLength(64)]
    public string Name { get; set; }
    public bool? DisplayInList { get; set; }
    public int? Position { get; set; }
    public int? RelationClassId { get; set; }
    public bool? IsRelationSingleParent { get; set; }
    public int? PropertyDataTypeId { get; set; }

    public virtual Class Class { get; set; }
    public virtual Tab Tab { get; set; }
    public virtual Class RelationClass { get; set; }
    public virtual DataType PropertyDataType { get; set; }
  }
}