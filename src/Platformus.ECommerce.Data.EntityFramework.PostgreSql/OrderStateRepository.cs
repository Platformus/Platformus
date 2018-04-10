// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.PostgreSql
{
  /// <summary>
  /// Implements the <see cref="IOrderStateRepository"/> interface and represents the repository
  /// for manipulating the <see cref="OrderState"/> entities in the context of PostgreSQL database.
  /// </summary>
  public class OrderStateRepository : RepositoryBase<OrderState>, IOrderStateRepository
  {
    /// <summary>
    /// Gets the order state by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order state.</param>
    /// <returns>Found order state with the given identifier.</returns>
    public OrderState WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the order state by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the order state.</param>
    /// <returns>Found order state with the given code.</returns>
    public OrderState WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(os => string.Equals(os.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the order states using sorting by position (ascending).
    /// </summary>
    /// <returns>Found order states.</returns>
    public IEnumerable<OrderState> All()
    {
      return this.dbSet.OrderBy(os => os.Position);
    }

    /// <summary>
    /// Gets all the order states using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The order state property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of order states that should be skipped.</param>
    /// <param name="take">The number of order states that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found order states using the given filtering, sorting, and paging.</returns>
    public IEnumerable<OrderState> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredOrderStates(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the order state.
    /// </summary>
    /// <param name="orderState">The order state to create.</param>
    public void Create(OrderState orderState)
    {
      this.dbSet.Add(orderState);
    }

    /// <summary>
    /// Edits the order state.
    /// </summary>
    /// <param name="orderState">The order state to edit.</param>
    public void Edit(OrderState orderState)
    {
      this.storageContext.Entry(orderState).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the order state specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order state to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the order state.
    /// </summary>
    /// <param name="orderState">The order state to delete.</param>
    public void Delete(OrderState orderState)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          CREATE TEMP TABLE ""TempDictionaries"" (""Id"" INT PRIMARY KEY);
          INSERT INTO ""TempDictionaries"" SELECT ""NameId"" FROM ""OrderStates"" WHERE ""Id"" = {0};
          DELETE FROM ""OrderStates"" WHERE ""Id"" = {0};
          DELETE FROM ""Localizations"" WHERE ""DictionaryId"" IN (SELECT ""Id"" FROM ""TempDictionaries"");
          DELETE FROM ""Dictionaries"" WHERE ""Id"" IN (SELECT ""Id"" FROM ""TempDictionaries"");
        ",
        orderState.Id
      );
    }

    /// <summary>
    /// Counts the number of the order states with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of order states found.</returns>
    public int Count(string filter)
    {
      return this.GetFilteredOrderStates(dbSet, filter).Count();
    }

    private IQueryable<OrderState> GetFilteredOrderStates(IQueryable<OrderState> orderStates, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return orderStates;

      return orderStates.Where(p => p.Code.ToLower().Contains(filter.ToLower()));
    }
  }
}