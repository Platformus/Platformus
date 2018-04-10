// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Cart"/> entities.
  /// </summary>
  public interface ICartRepository : IRepository
  {
    /// <summary>
    /// Gets the cart by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart.</param>
    /// <returns>Found cart with the given identifier.</returns>
    Cart WithKey(int id);

    /// <summary>
    /// Gets the cart by the client-side identifier (case insensitive).
    /// </summary>
    /// <param name="clientSideId">The unique client-side identifier of the cart.</param>
    /// <returns>Found cart with the given client-side identifier.</returns>
    Cart WithClientSideId(string clientSideId);

    /// <summary>
    /// Gets the carts filtered by the order identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="orderId">The unique identifier of the order these carts belongs to.</param>
    /// <returns>Found carts.</returns>
    IEnumerable<Cart> FilteredByOrderId(int orderId);

    /// <summary>
    /// Gets all the carts using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The cart property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of carts that should be skipped.</param>
    /// <param name="take">The number of carts that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found carts using the given filtering, sorting, and paging.</returns>
    IEnumerable<Cart> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the cart.
    /// </summary>
    /// <param name="cart">The cart to create.</param>
    void Create(Cart cart);

    /// <summary>
    /// Edits the cart.
    /// </summary>
    /// <param name="cart">The cart to edit.</param>
    void Edit(Cart cart);

    /// <summary>
    /// Deletes the cart specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the cart.
    /// </summary>
    /// <param name="cart">The cart to delete.</param>
    void Delete(Cart cart);

    /// <summary>
    /// Counts the number of the carts with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of carts found.</returns>
    int Count(string filter);
  }
}