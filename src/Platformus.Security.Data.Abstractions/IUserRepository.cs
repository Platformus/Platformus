// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="User"/> entities.
  /// </summary>
  public interface IUserRepository : IRepository
  {
    /// <summary>
    /// Gets the user by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>Found user with the given identifier.</returns>
    User WithKey(int id);

    /// <summary>
    /// Gets all the users using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The user property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of users that should be skipped.</param>
    /// <param name="take">The number of users that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found users using the given filtering, sorting, and paging.</returns>
    IEnumerable<User> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the user.
    /// </summary>
    /// <param name="user">The user to create.</param>
    void Create(User user);

    /// <summary>
    /// Edits the user.
    /// </summary>
    /// <param name="user">The user to edit.</param>
    void Edit(User user);

    /// <summary>
    /// Deletes the user specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the user.
    /// </summary>
    /// <param name="user">The user to delete.</param>
    void Delete(User user);

    /// <summary>
    /// Counts the number of the users with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of users found.</returns>
    int Count(string filter);
  }
}