// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.PostgreSql
{
  /// <summary>
  /// Implements the <see cref="IProductCatalogRepository"/> interface and represents the repository
  /// for manipulating the <see cref="ProductCatalog"/> entities in the context of PostgreSQL database.
  /// </summary>
  public class ProductCatalogRepository : RepositoryBase<ProductCatalog>, IProductCatalogRepository
  {
    /// <summary>
    /// Gets the product catalog by the product identifier and catalog identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product this product catalog is related to.</param>
    /// <param name="catalogId">The unique identifier of the catalog this product catalog is related to.</param>
    /// <returns>Found product catalog with the given product identifier and catalog identifier.</returns>
    public ProductCatalog WithKey(int productId, int catalogId)
    {
      return this.dbSet.Find(productId, catalogId);
    }

    /// <summary>
    /// Gets the product catalog filtered by the product identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product these product catalogs belongs to.</param>
    public IEnumerable<ProductCatalog> FilteredByProductId(int productId)
    {
      return this.dbSet.AsNoTracking().Where(pc => pc.ProductId == productId);
    }

    /// <summary>
    /// Creates the product catalog.
    /// </summary>
    /// <param name="productCatalog">The product catalog to create.</param>
    public void Create(ProductCatalog productCatalog)
    {
      this.dbSet.Add(productCatalog);
    }

    /// <summary>
    /// Edits the product catalog.
    /// </summary>
    /// <param name="productCatalog">The product catalog to edit.</param>
    public void Edit(ProductCatalog productCatalog)
    {
      this.storageContext.Entry(productCatalog).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the product catalog specified by the product identifier and catalog identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product this product catalog is related to.</param>
    /// <param name="catalogId">The unique identifier of the catalog this product catalog is related to.</param>
    public void Delete(int productId, int catalogId)
    {
      this.Delete(this.WithKey(productId, catalogId));
    }

    /// <summary>
    /// Deletes the product catalog.
    /// </summary>
    /// <param name="productCatalog">The product catalog to delete.</param>
    public void Delete(ProductCatalog productCatalog)
    {
      this.dbSet.Remove(productCatalog);
    }
  }
}