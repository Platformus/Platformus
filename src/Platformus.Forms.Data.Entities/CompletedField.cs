// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Forms.Data.Entities
{
  /// <summary>
  /// Represents a completed field. The completed fields are used to store the user input for further processing.
  /// </summary>
  public class CompletedField : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the completed field.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the completed form identifier this completed field belongs to.
    /// </summary>
    public int CompletedFormId { get; set; }

    /// <summary>
    /// Gets or sets the field identifier this completed field is related to.
    /// </summary>
    public int FieldId { get; set; }

    /// <summary>
    /// Gets or sets the completed field value.
    /// </summary>
    public string Value { get; set; }

    public virtual CompletedForm CompletedForm { get; set; }
    public virtual Field Field { get; set; }
  }
}