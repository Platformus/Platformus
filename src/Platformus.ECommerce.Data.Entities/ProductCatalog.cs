// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.ECommerce.Data.Entities
{
  /// <summary>
  /// Represents a many-to-many relationship between the products and catalogs.
  /// </summary>
  public class ProductCatalog : IEntity
  {
    /// <summary>
    /// Gets or sets the product identifier this product catalog is related to.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the catalog identifier this product catalog is related to.
    /// </summary>
    public int CatalogId { get; set; }

    public virtual Product Product { get; set; }
    public virtual Catalog Catalog { get; set; }
  }
}