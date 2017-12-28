// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Forms.Data.Entities
{
  /// <summary>
  /// Represents a field. The fields are used to build the forms.
  /// </summary>
  public class Field : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the field.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the form identifier this field belongs to.
    /// </summary>
    public int FormId { get; set; }

    /// <summary>
    /// Gets or sets the field type identifier this field is related to.
    /// </summary>
    public int FieldTypeId { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the field. It is set by the user and might be used for the field retrieval or data binding.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this field is related to. It is used to store the localizable field name.
    /// </summary>
    public int NameId { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the field is required or not.
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// Gets or sets the maximum text length the field can contain.
    /// </summary>
    public int? MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the field position. Position is used to sort the fields inside the form (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual Form Form { get; set; }
    public virtual FieldType FieldType { get; set; }
    public virtual Dictionary Name { get; set; }
    public virtual ICollection<FieldOption> FieldOptions { get; set; }
  }
}