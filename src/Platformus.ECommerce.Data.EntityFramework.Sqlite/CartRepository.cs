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

namespace Platformus.ECommerce.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="ICartRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Cart"/> entities in the context of SQLite database.
  /// </summary>
  public class CartRepository : RepositoryBase<Cart>, ICartRepository
  {
    /// <summary>
    /// Gets the cart by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart.</param>
    /// <returns>Found cart with the given identifier.</returns>
    public Cart WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the cart by the client-side identifier (case insensitive).
    /// </summary>
    /// <param name="clientSideId">The unique client-side identifier of the cart.</param>
    /// <returns>Found cart with the given client-side identifier.</returns>
    public Cart WithClientSideId(string clientSideId)
    {
      return this.dbSet.FirstOrDefault(c => string.Equals(c.ClientSideId, clientSideId, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets the carts filtered by the order identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="orderId">The unique identifier of the order these carts belongs to.</param>
    /// <returns>Found carts.</returns>
    public IEnumerable<Cart> FilteredByOrderId(int orderId)
    {
      return this.dbSet.Where(c => c.OrderId == orderId);
    }

    /// <summary>
    /// Gets all the carts using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The cart property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of carts that should be skipped.</param>
    /// <param name="take">The number of carts that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found carts using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Cart> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredCarts(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the cart.
    /// </summary>
    /// <param name="cart">The cart to create.</param>
    public void Create(Cart cart)
    {
      this.dbSet.Add(cart);
    }

    /// <summary>
    /// Edits the cart.
    /// </summary>
    /// <param name="cart">The cart to edit.</param>
    public void Edit(Cart cart)
    {
      this.storageContext.Entry(cart).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the cart specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the cart.
    /// </summary>
    /// <param name="cart">The cart to delete.</param>
    public void Delete(Cart cart)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM Positions WHERE CartId = {0};
          DELETE FROM Carts WHERE Id = {0};
        ",
        cart.Id
      );
    }

    /// <summary>
    /// Counts the number of the carts with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of carts found.</returns>
    public int Count(string filter)
    {
      return this.GetFilteredCarts(dbSet, filter).Count();
    }

    private IQueryable<Cart> GetFilteredCarts(IQueryable<Cart> carts, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return carts;

      return carts.Where(c => true);
    }
  }
}