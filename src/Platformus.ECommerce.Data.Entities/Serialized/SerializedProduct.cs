// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents a serialized product. The serialized products contain the product and the corresponding attributes
  /// inside the single entity. This reduces the number of storage read operations while product retrieval.
  /// </summary>
  public class SerializedProduct : IEntity
  {
    /// <summary>
    /// Gets or sets the culture identifier this serialized product belongs to.
    /// </summary>
    public int CultureId { get; set; }

    /// <summary>
    /// Gets or sets the product identifier this serialized product belongs to.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the category identifier this product belongs to.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the unique URL of the product.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the product. It is set by the user and might be used for the product retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the product description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the product price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the product title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the product META description.
    /// </summary>
    public string MetaDescription { get; set; }

    /// <summary>
    /// Gets or sets the product META keywords.
    /// </summary>
    public string MetaKeywords { get; set; }

    /// <summary>
    /// Gets or sets the attributes serialized into a string.
    /// </summary>
    public string SerializedAttributes { get; set; }

    /// <summary>
    /// Gets or sets the photos serialized into a string.
    /// </summary>
    public string SerializedPhotos { get; set; }

    public virtual Culture Culture { get; set; }
    public virtual Product Product { get; set; }
  }
}