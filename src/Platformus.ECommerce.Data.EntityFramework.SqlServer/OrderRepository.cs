// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
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
  /// Implements the <see cref="IOrderRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Order"/> entities in the context of SQL Server database.
  /// </summary>
  public class OrderRepository : RepositoryBase<Order>, IOrderRepository
  {
    /// <summary>
    /// Gets the order by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order.</param>
    /// <returns>Found order with the given identifier.</returns>
    public Order WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets all the orders using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The order property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of orders that should be skipped.</param>
    /// <param name="take">The number of orders that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found orders using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Order> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredOrders(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the order.
    /// </summary>
    /// <param name="order">The order to create.</param>
    public void Create(Order order)
    {
      this.dbSet.Add(order);
    }

    /// <summary>
    /// Edits the order.
    /// </summary>
    /// <param name="order">The order to edit.</param>
    public void Edit(Order order)
    {
      this.storageContext.Entry(order).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the order specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the order.
    /// </summary>
    /// <param name="order">The order to delete.</param>
    public void Delete(Order order)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM Positions WHERE OrderId = {0};
          DELETE FROM Orders WHERE Id = {0};
        ",
        order.Id
      );
    }

    /// <summary>
    /// Counts the number of the orders with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of orders found.</returns>
    public int Count(string filter)
    {
      return this.GetFilteredOrders(dbSet, filter).Count();
    }

    private IQueryable<Order> GetFilteredOrders(IQueryable<Order> orders, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return orders;

      return orders.Where(o => true);
    }
  }
}