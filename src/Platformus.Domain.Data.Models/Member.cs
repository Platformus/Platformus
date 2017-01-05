// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Domain.Data.Models
{
  public class Member : IEntity
  {
    public int Id { get; set; }
    public int ClassId { get; set; }
    public int? TabId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }
    public int? PropertyDataTypeId { get; set; }
    public bool? IsPropertyLocalizable { get; set; }
    public bool? IsPropertyVisibleInList { get; set; }
    public int? RelationClassId { get; set; }
    public bool? IsRelationSingleParent { get; set; }

    public virtual Class Class { get; set; }
    public virtual Tab Tab { get; set; }
    public virtual Class RelationClass { get; set; }
    public virtual DataType PropertyDataType { get; set; }
  }
}