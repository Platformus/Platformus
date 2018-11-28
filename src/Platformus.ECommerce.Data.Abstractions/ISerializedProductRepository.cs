// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="SerializedProduct"/> entities.
  /// </summary>
  public interface ISerializedProductRepository : IRepository
  {
    /// <summary>
    /// Gets the serialized product by the culture identifier and product identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized product belongs to.</param>
    /// <param name="productId">The unique identifier of the product this serialized product belongs to.</param>
    /// <returns>Found serialized product with the given culture identifier and product identifier.</returns>
    SerializedProduct WithKey(int cultureId, int productId);

    /// <summary>
    /// Gets the serialized product by the URL (case insensitive).
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns>Found serialized product with the given URL.</returns>
    SerializedProduct WithUrl(string url);

    /// <summary>
    /// Gets all the serialized products filtered by the culture identifier and category identifier using the given sorting and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized products belongs to.</param>
    /// <param name="categoryId">The unique identifier of the category these serialized products belongs to.</param>
    /// <param name="orderBy">The serialized product property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of serialized products that should be skipped.</param>
    /// <param name="take">The number of serialized products that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found serialized products filtered by the culture identifier and category identifier using the given filtering, sorting, and paging.</returns>
    IEnumerable<SerializedProduct> FilteredByCultureIdAndCategoryIdRange(int cultureId, int categoryId, string orderBy, string direction, int skip, int take);

    /// <summary>
    /// Gets all the serialized products filtered by the culture identifier, category identifier, and attribute identifiers using the given sorting and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized products belongs to.</param>
    /// <param name="categoryId">The unique identifier of the category these serialized products belongs to.</param>
    /// <param name="attributeIds">The unique identifier of the attributes these serialized products have.</param>
    /// <param name="orderBy">The serialized product property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of serialized products that should be skipped.</param>
    /// <param name="take">The number of serialized products that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found serialized products filtered by the culture identifier and category identifier using the given filtering, sorting, and paging.</returns>
    IEnumerable<SerializedProduct> FilteredByCultureIdAndCategoryIdAndAttributeIdsRange(int cultureId, int categoryId, int[] attributeIds, string orderBy, string direction, int skip, int take);

    /// <summary>
    /// Creates the serialized product.
    /// </summary>
    /// <param name="serializedProduct">The serialized product to create.</param>
    void Create(SerializedProduct serializedProduct);

    /// <summary>
    /// Edits the serialized product.
    /// </summary>
    /// <param name="serializedProduct">The serialized product to edit.</param>
    void Edit(SerializedProduct serializedProduct);

    /// <summary>
    /// Deletes the serialized product specified by the culture identifier and product identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized product belongs to.</param>
    /// <param name="productId">The unique identifier of the product this serialized product belongs to.</param>
    void Delete(int cultureId, int productId);

    /// <summary>
    /// Deletes the serialized product.
    /// </summary>
    /// <param name="serializedProduct">The serialized product to delete.</param>
    void Delete(SerializedProduct serializedProduct);
  }
}