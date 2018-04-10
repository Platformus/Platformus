// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Product"/> entities.
  /// </summary>
  public interface IProductRepository : IRepository
  {
    /// <summary>
    /// Gets the product by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <returns>Found product with the given identifier.</returns>
    Product WithKey(int id);

    /// <summary>
    /// Gets the product by the URL (case insensitive).
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns>Found product with the given URL.</returns>
    Product WithUrl(string url);

    /// <summary>
    /// Gets the product by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the product.</param>
    /// <returns>Found product with the given code.</returns>
    Product WithCode(string code);

    /// <summary>
    /// Gets all the products using sorting by code (ascending).
    /// </summary>
    /// <returns>Found products.</returns>
    IEnumerable<Product> All();

    /// <summary>
    /// Gets all the products using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The product property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of products that should be skipped.</param>
    /// <param name="take">The number of products that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found products using the given filtering, sorting, and paging.</returns>
    IEnumerable<Product> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Gets all the products filtered by the category identifier using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category these products belongs to.</param>
    /// <param name="orderBy">The product property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of products that should be skipped.</param>
    /// <param name="take">The number of products that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found products filtered by the category identifier using the given filtering, sorting, and paging.</returns>
    IEnumerable<Product> FilteredByCategoryIdRange(int categoryId, string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the product.
    /// </summary>
    /// <param name="product">The product to create.</param>
    void Create(Product product);

    /// <summary>
    /// Edits the product.
    /// </summary>
    /// <param name="product">The product to edit.</param>
    void Edit(Product product);

    /// <summary>
    /// Deletes the product specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the product.
    /// </summary>
    /// <param name="product">The product to delete.</param>
    void Delete(Product product);

    /// <summary>
    /// Counts the number of the products with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of products found.</returns>
    int Count(string filter);
  }
}