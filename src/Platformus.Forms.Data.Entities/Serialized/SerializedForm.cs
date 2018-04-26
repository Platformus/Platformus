// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Forms.Data.Entities
{
  /// <summary>
  /// Represents a serialized form. The serialized forms contain the form and corresponding fields data
  /// inside the single entity. This reduces the number of storage read operations while form retrieval.
  /// </summary>
  public class SerializedForm : IEntity
  {
    /// <summary>
    /// Gets or sets the culture identifier this serialized form belongs to.
    /// </summary>
    public int CultureId { get; set; }

    /// <summary>
    /// Gets or sets the form identifier this serialized form belongs to.
    /// </summary>
    public int FormId { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the form. It is set by the user and might be used for the form retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the form name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the submit button title of the form.
    /// </summary>
    public string SubmitButtonTitle { get; set; }

    /// <summary>
    /// Gets or sets the fields serialized into a string.
    /// </summary>
    public string SerializedFields { get; set; }

    public virtual Culture Culture { get; set; }
    public virtual Form Form { get; set; }
  }
}