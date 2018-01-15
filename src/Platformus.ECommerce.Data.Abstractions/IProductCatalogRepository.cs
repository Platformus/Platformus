// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="ProductCatalog"/> entities.
  /// </summary>
  public interface IProductCatalogRepository : IRepository
  {
    /// <summary>
    /// Gets the product catalog by the product identifier and catalog identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product this product catalog is related to.</param>
    /// <param name="catalogId">The unique identifier of the catalog this product catalog is related to.</param>
    /// <returns>Found product catalog with the given product identifier and catalog identifier.</returns>
    ProductCatalog WithKey(int productId, int catalogId);

    /// <summary>
    /// Gets the product catalog filtered by the product identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product these product catalogs belongs to.</param>
    IEnumerable<ProductCatalog> FilteredByProductId(int productId);

    /// <summary>
    /// Creates the product catalog.
    /// </summary>
    /// <param name="productCatalog">The product catalog to create.</param>
    void Create(ProductCatalog productCatalog);

    /// <summary>
    /// Edits the product catalog.
    /// </summary>
    /// <param name="productCatalog">The product catalog to edit.</param>
    void Edit(ProductCatalog productCatalog);

    /// <summary>
    /// Deletes the product catalog specified by the product identifier and catalog identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product this product catalog is related to.</param>
    /// <param name="catalogId">The unique identifier of the catalog this product catalog is related to.</param>
    void Delete(int productId, int catalogId);

    /// <summary>
    /// Deletes the product catalog.
    /// </summary>
    /// <param name="productCatalog">The product catalog to delete.</param>
    void Delete(ProductCatalog productCatalog);
  }
}