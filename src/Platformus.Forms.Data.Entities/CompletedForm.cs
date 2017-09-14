// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Forms.Data.Entities
{
  /// <summary>
  /// Represents a completed form. The completed forms are used to store the user input for further processing.
  /// </summary>
  public class CompletedForm : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the completed form.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the form identifier this completed form belongs to.
    /// </summary>
    public int FormId { get; set; }

    /// <summary>
    /// Gets or sets the date and time this completed form is created at.
    /// </summary>
    public DateTime Created { get; set; }

    public virtual Form Form { get; set; }
    public virtual ICollection<CompletedField> CompletedFields { get; set; }
  }
}