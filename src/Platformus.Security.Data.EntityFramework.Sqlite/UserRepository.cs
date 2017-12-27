// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="IUserRepository"/> interface and represents the repository
  /// for manipulating the <see cref="User"/> entities in the context of SQLite database.
  /// </summary>
  public class UserRepository : RepositoryBase<User>, IUserRepository
  {
    /// <summary>
    /// Gets the user by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>Found user with the given identifier.</returns>
    public User WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets all the users using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The user property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of users that should be skipped.</param>
    /// <param name="take">The number of users that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found users using the given filtering, sorting, and paging.</returns>
    public IEnumerable<User> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredUsers(this.dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    public void Create(User user)
    {
      this.dbSet.Add(user);
    }

    /// <summary>
    /// Edits the user.
    /// </summary>
    /// <param name="user">The user to edit.</param>
    public void Edit(User user)
    {
      this.storageContext.Entry(user).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the user specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the user.
    /// </summary>
    /// <param name="user">The user to delete.</param>
    public void Delete(User user)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM UserRoles WHERE UserId = {0};
          DELETE FROM Credentials WHERE UserId = {0};
        ",
        user.Id
      );

      this.dbSet.Remove(user);
    }

    /// <summary>
    /// Counts the number of the users with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of users found.</returns>
    public int Count(string filter)
    {
      return this.GetFilteredUsers(this.dbSet, filter).Count();
    }

    private IQueryable<User> GetFilteredUsers(IQueryable<User> users, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return users;

      return users.Where(u => u.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}