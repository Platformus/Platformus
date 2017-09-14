// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Forms.Data.Entities
{
  /// <summary>
  /// Represents a field type. The field types are used to specify how the field should look and behave,
  /// and how it should be processed.
  /// </summary>
  public class FieldType : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the field type.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the field type. It is set by the user and might be used for the field type retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the field type name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the field type position. Position is used to sort the field types (smallest to largest).
    /// </summary>
    public int? Position { get; set; }
  }
}