// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Order"/> entities.
  /// </summary>
  public interface IOrderRepository : IRepository
  {
    /// <summary>
    /// Gets the order by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order.</param>
    /// <returns>Found order with the given identifier.</returns>
    Order WithKey(int id);

    /// <summary>
    /// Gets all the orders using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The order property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of orders that should be skipped.</param>
    /// <param name="take">The number of orders that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found orders using the given filtering, sorting, and paging.</returns>
    IEnumerable<Order> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the order.
    /// </summary>
    /// <param name="order">The order to create.</param>
    void Create(Order order);

    /// <summary>
    /// Edits the order.
    /// </summary>
    /// <param name="order">The order to edit.</param>
    void Edit(Order order);

    /// <summary>
    /// Deletes the order specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the order.
    /// </summary>
    /// <param name="order">The order to delete.</param>
    void Delete(Order order);

    /// <summary>
    /// Counts the number of the orders with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of orders found.</returns>
    int Count(string filter);
  }
}