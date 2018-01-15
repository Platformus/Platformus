// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents a product.
  /// </summary>
  public class Product : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the category identifier this product belongs to.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the product. It is set by the user and might be used for the product retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this field is related to. It is used to store the localizable product name.
    /// </summary>
    public int NameId { get; set; }

    /// <summary>
    /// Gets or sets the product price.
    /// </summary>
    public decimal Price { get; set; }

    public virtual Category Category { get; set; }
    public virtual Dictionary Name { get; set; }
  }
}