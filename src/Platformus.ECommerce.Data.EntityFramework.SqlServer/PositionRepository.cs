// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IPositionRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Position"/> entities in the context of SQL Server database.
  /// </summary>
  public class PositionRepository : RepositoryBase<Position>, IPositionRepository
  {
    /// <summary>
    /// Gets the position by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the position.</param>
    /// <returns>Found position with the given identifier.</returns>
    public Position WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the positions filtered by the order identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="orderId">The unique identifier of the order these positions belongs to.</param>
    /// <returns>Found positions.</returns>
    public IEnumerable<Position> FilteredByOrderId(int orderId)
    {
      return this.dbSet.AsNoTracking().Where(p => p.OrderId == orderId).OrderBy(p => p.Id);
    }

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
    public IEnumerable<Position> Range(int orderId, string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredPositions(dbSet.AsNoTracking(), orderId, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the position.
    /// </summary>
    /// <param name="position">The position to create.</param>
    public void Create(Position position)
    {
      this.dbSet.Add(position);
    }

    /// <summary>
    /// Edits the position.
    /// </summary>
    /// <param name="position">The position to edit.</param>
    public void Edit(Position position)
    {
      this.storageContext.Entry(position).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the position specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the position to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the position.
    /// </summary>
    /// <param name="position">The position to delete.</param>
    public void Delete(Position position)
    {
      this.dbSet.Remove(position);
    }

    /// <summary>
    /// Counts the number of the positions filtered by the order identifier with the given filtering.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order these positions belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of positions found.</returns>
    public int Count(int orderId, string filter)
    {
      return this.GetFilteredPositions(dbSet, orderId, filter).Count();
    }

    private IQueryable<Position> GetFilteredPositions(IQueryable<Position> positions, int orderId, string filter)
    {
      positions = positions.Where(p => p.OrderId == orderId);

      if (string.IsNullOrEmpty(filter))
        return positions;

      return positions.Where(p => true);
    }
  }
}