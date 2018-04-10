// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Forms.Data.Entities
{
  /// <summary>
  /// Represents a field option. The field options are used to build the fields.
  /// </summary>
  public class FieldOption : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the field option.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the field identifier this field option belongs to.
    /// </summary>
    public int FieldId { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this field option is related to. It is used to store the localizable field option value.
    /// </summary>
    public int ValueId { get; set; }

    /// <summary>
    /// Gets or sets the field option position. Position is used to sort the field options inside the field (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual Field Field { get; set; }
    public virtual Dictionary Value { get; set; }
  }
}