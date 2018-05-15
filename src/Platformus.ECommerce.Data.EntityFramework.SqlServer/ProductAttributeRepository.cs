// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IProductAttributeRepository"/> interface and represents the repository
  /// for manipulating the <see cref="ProductAttribute"/> entities in the context of SQL Server database.
  /// </summary>
  public class ProductAttributeRepository : RepositoryBase<ProductAttribute>, IProductAttributeRepository
  {
    /// <summary>
    /// Gets the product attribute by the product identifier and attribute identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product this product attribute is related to.</param>
    /// <param name="attributeId">The unique identifier of the attribute this product attribute is related to.</param>
    /// <returns>Found product attribute with the given product identifier and attribute identifier.</returns>
    public ProductAttribute WithKey(int productId, int attributeId)
    {
      return this.dbSet.Find(productId, attributeId);
    }

    /// <summary>
    /// Gets the product attribute filtered by the product identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product these product attributes belongs to.</param>
    public IEnumerable<ProductAttribute> FilteredByProductId(int productId)
    {
      return this.dbSet.AsNoTracking().Where(ur => ur.ProductId == productId);
    }

    /// <summary>
    /// Creates the product attribute.
    /// </summary>
    /// <param name="productAttribute">The product attribute to create.</param>
    public void Create(ProductAttribute productAttribute)
    {
      this.dbSet.Add(productAttribute);
    }

    /// <summary>
    /// Edits the product attribute.
    /// </summary>
    /// <param name="productAttribute">The product attribute to edit.</param>
    public void Edit(ProductAttribute productAttribute)
    {
      this.storageContext.Entry(productAttribute).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the product attribute specified by the product identifier and attribute identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product this product attribute is related to.</param>
    /// <param name="attributeId">The unique identifier of the attribute this product attribute is related to.</param>
    public void Delete(int productId, int attributeId)
    {
      this.Delete(this.WithKey(productId, attributeId));
    }

    /// <summary>
    /// Deletes the product attribute.
    /// </summary>
    /// <param name="productAttribute">The product attribute to delete.</param>
    public void Delete(ProductAttribute productAttribute)
    {
      this.dbSet.Remove(productAttribute);
    }
  }
}