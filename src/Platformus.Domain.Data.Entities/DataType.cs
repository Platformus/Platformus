// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Domain.Data.Entities
{
  /// <summary>
  /// Represents a data type. The data types are used to specify how the object property values
  /// should be stored, displayed, and edited.
  /// </summary>
  public class DataType : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the data type.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the data type storage data type. See the <see cref="Platformus.Domain.Data.Entities.StorageDataType"/>
    /// class for the predefined constants.
    /// </summary>
    public string StorageDataType { get; set; }

    /// <summary>
    /// Gets or sets the JavaScript editor class name (name of the JavaScript function that is used to build the editor).
    /// </summary>
    public string JavaScriptEditorClassName { get; set; }

    /// <summary>
    /// Gets or sets the data type name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the data type position. Position is used to sort the data types (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual ICollection<Member> Members { get; set; }
    public virtual ICollection<DataTypeParameter> DataTypeParameters { get; set; }
  }
}