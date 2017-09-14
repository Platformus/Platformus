// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Domain.Data.Entities
{
  /// <summary>
  /// Represents a serialized object. The serialized objects contain the object and the corresponding property values
  /// inside the single entity. This reduces the number of storage read operations while object retrieval.
  /// </summary>
  public class SerializedObject : IEntity
  {
    /// <summary>
    /// Gets or sets the culture identifier this serialized object belongs to.
    /// </summary>
    public int CultureId { get; set; }

    /// <summary>
    /// Gets or sets the object identifier this serialized object belongs to.
    /// </summary>
    public int ObjectId { get; set; }

    /// <summary>
    /// Gets or sets the class identifier this object belongs to.
    /// </summary>
    public int ClassId { get; set; }

    /// <summary>
    /// Gets or sets the URL object property value (if exists).
    /// </summary>
    public string UrlPropertyStringValue { get; set; }

    /// <summary>
    /// Gets or sets the properties serialized into a string.
    /// </summary>
    public string SerializedProperties { get; set; }

    public virtual Culture Culture { get; set; }
    public virtual Object Object { get; set; }
    public virtual Class Class { get; set; }
  }
}