// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents a photo.
  /// </summary>
  public class Photo : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the photo.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the photo identifier this product belongs to.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the physical filename of the photo.
    /// </summary>
    public string Filename { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the photo is cover one or not.
    /// </summary>
    public bool IsCover { get; set; }

    /// <summary>
    /// Gets or sets the photo position. Position is used to sort the photos inside the product (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual Product Product { get; set; }
  }
}