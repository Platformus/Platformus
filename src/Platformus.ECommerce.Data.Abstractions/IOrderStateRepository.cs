// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="OrderState"/> entities.
  /// </summary>
  public interface IOrderStateRepository : IRepository
  {
    /// <summary>
    /// Gets the order state by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order state.</param>
    /// <returns>Found order state with the given identifier.</returns>
    OrderState WithKey(int id);

    /// <summary>
    /// Gets the order state by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the order state.</param>
    /// <returns>Found order state with the given code.</returns>
    OrderState WithCode(string code);

    /// <summary>
    /// Gets all the order states using sorting by position (ascending).
    /// </summary>
    /// <returns>Found order states.</returns>
    IEnumerable<OrderState> All();

    /// <summary>
    /// Gets all the order states using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The order state property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of order states that should be skipped.</param>
    /// <param name="take">The number of order states that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found order states using the given filtering, sorting, and paging.</returns>
    IEnumerable<OrderState> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the order state.
    /// </summary>
    /// <param name="orderState">The order state to create.</param>
    void Create(OrderState orderState);

    /// <summary>
    /// Edits the order state.
    /// </summary>
    /// <param name="orderState">The order state to edit.</param>
    void Edit(OrderState orderState);

    /// <summary>
    /// Deletes the order state specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order state to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the order state.
    /// </summary>
    /// <param name="orderState">The order state to delete.</param>
    void Delete(OrderState orderState);

    /// <summary>
    /// Counts the number of the order states with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of order states found.</returns>
    int Count(string filter);
  }
}