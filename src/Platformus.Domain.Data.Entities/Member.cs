// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Domain.Data.Entities
{
  /// <summary>
  /// Represents a member. The members are used to describe which properties and relations
  /// the objects of a given class should have.
  /// </summary>
  public class Member : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the member.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the class identifier this member belongs to.
    /// </summary>
    public int ClassId { get; set; }

    /// <summary>
    /// Gets or sets the tab identifier this member is related to.
    /// </summary>
    public int? TabId { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the member. It is set by the user and might be used for the member retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the member name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the member position. Position is used to sort the members inside the class (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    /// <summary>
    /// Gets or sets the data type identifier this member properties should have.
    /// </summary>
    public int? PropertyDataTypeId { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether this member properties are localizable or not. Localizable properties
    /// store different values for the different cultures, while not localizable ones store the same value for all the cultures.
    /// </summary>
    public bool? IsPropertyLocalizable { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether this member properties are visible in the list of the corresponding objects or not.
    /// </summary>
    public bool? IsPropertyVisibleInList { get; set; }

    /// <summary>
    /// Gets or sets the class identifier this member relations should have.
    /// </summary>
    public int? RelationClassId { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether this member relations are single parent or not.
    /// </summary>
    public bool? IsRelationSingleParent { get; set; }

    /// <summary>
    /// Gets or sets the minimum number of the related objects this member relations can have.
    /// </summary>
    public int? MinRelatedObjectsNumber { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of the related objects this member relations can have.
    /// </summary>
    public int? MaxRelatedObjectsNumber { get; set; }

    public virtual Class Class { get; set; }
    public virtual Tab Tab { get; set; }
    public virtual Class RelationClass { get; set; }
    public virtual DataType PropertyDataType { get; set; }
  }
}