// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents an attribute.
  /// </summary>
  public class Attribute : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the attribute.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the feature identifier this attribute belongs to.
    /// </summary>
    public int FeatureId { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this attribute is related to. It is used to store the localizable attribute value.
    /// </summary>
    public int ValueId { get; set; }

    /// <summary>
    /// Gets or sets the attribute position. Position is used to sort the attributes inside the feature (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual Feature Feature { get; set; }
    public virtual Dictionary Value { get; set; }
  }
}