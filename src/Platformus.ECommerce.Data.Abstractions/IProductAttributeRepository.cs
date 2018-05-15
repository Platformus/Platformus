// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="ProductAttribute"/> entities.
  /// </summary>
  public interface IProductAttributeRepository : IRepository
  {
    /// <summary>
    /// Gets the product attribute by the product identifier and attribute identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product this product attribute is related to.</param>
    /// <param name="attributeId">The unique identifier of the attribute this product attribute is related to.</param>
    /// <returns>Found product attribute with the given product identifier and attribute identifier.</returns>
    ProductAttribute WithKey(int productId, int attributeId);

    /// <summary>
    /// Gets the product attribute filtered by the product identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product these product attributes belongs to.</param>
    IEnumerable<ProductAttribute> FilteredByProductId(int productId);

    /// <summary>
    /// Creates the product attribute.
    /// </summary>
    /// <param name="productAttribute">The product attribute to create.</param>
    void Create(ProductAttribute productAttribute);

    /// <summary>
    /// Edits the product attribute.
    /// </summary>
    /// <param name="productAttribute">The product attribute to edit.</param>
    void Edit(ProductAttribute productAttribute);

    /// <summary>
    /// Deletes the product attribute specified by the product identifier and attribute identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product this product attribute is related to.</param>
    /// <param name="attributeId">The unique identifier of the attribute this product attribute is related to.</param>
    void Delete(int productId, int attributeId);

    /// <summary>
    /// Deletes the product attribute.
    /// </summary>
    /// <param name="productAttribute">The product attribute to delete.</param>
    void Delete(ProductAttribute productAttribute);
  }
}