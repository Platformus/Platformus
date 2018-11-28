// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents a serialized attribute. The serialized attributes contain the attribute and the corresponding feature
  /// inside the single entity. This reduces the number of storage read operations while attribute retrieval.
  /// </summary>
  public class SerializedAttribute : IEntity
  {
    public class Feature
    {
      public string Code { get; set; }
      public string Name { get; set; }
      public int? Position { get; set; }
    }

    /// <summary>
    /// Gets or sets the culture identifier this serialized attribute belongs to.
    /// </summary>
    public int CultureId { get; set; }

    /// <summary>
    /// Gets or sets the attribute identifier this serialized attribute belongs to.
    /// </summary>
    public int AttributeId { get; set; }

    /// <summary>
    /// Gets or sets the attribute value.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Gets or sets the attribute position.
    /// </summary>
    public int? Position { get; set; }

    /// <summary>
    /// Gets or sets the feature serialized into a string.
    /// </summary>
    public string SerializedFeature { get; set; }
  }
}