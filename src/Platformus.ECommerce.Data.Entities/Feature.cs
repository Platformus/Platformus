// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents a feature.
  /// </summary>
  public class Feature : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the feature.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the feature. It is set by the user and might be used for the feature retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this feature is related to. It is used to store the localizable feature name.
    /// </summary>
    public int NameId { get; set; }

    /// <summary>
    /// Gets or sets the feature position. Position is used to sort the features (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual Dictionary Name { get; set; }
  }
}