// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Position"/> entities.
  /// </summary>
  public interface IPositionRepository : IRepository
  {
    /// <summary>
    /// Gets the position by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the position.</param>
    /// <returns>Found position with the given identifier.</returns>
    Position WithKey(int id);

    /// <summary>
    /// Gets the positions filtered by the order identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="orderId">The unique identifier of the order these positions belongs to.</param>
    /// <returns>Found positions.</returns>
    IEnumerable<Position> FilteredByOrderId(int orderId);

    /// <summary>
    /// Gets the positions filtered by the order identifier using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order these positions belongs to.</param>
    /// <param name="orderBy">The position property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of positions that should be skipped.</param>
    /// <param name="take">The number of positions that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found positions using the given filtering, sorting, and paging.</returns>
    IEnumerable<Position> Range(int orderId, string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the position.
    /// </summary>
    /// <param name="position">The position to create.</param>
    void Create(Position position);

    /// <summary>
    /// Edits the position.
    /// </summary>
    /// <param name="position">The position to edit.</param>
    void Edit(Position position);

    /// <summary>
    /// Deletes the position specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the position to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the position.
    /// </summary>
    /// <param name="position">The position to delete.</param>
    void Delete(Position position);

    /// <summary>
    /// Counts the number of the positions filtered by the order identifier with the given filtering.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order these positions belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of positions found.</returns>
    int Count(int orderId, string filter);
  }
}