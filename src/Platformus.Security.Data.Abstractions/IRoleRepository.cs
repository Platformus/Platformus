// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Role"/> entities.
  /// </summary>
  public interface IRoleRepository : IRepository
  {
    /// <summary>
    /// Gets the role by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the role.</param>
    /// <returns>Found role with the given identifier.</returns>
    Role WithKey(int id);

    /// <summary>
    /// Gets the role by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the role.</param>
    /// <returns>Found role with the given code.</returns>
    Role WithCode(string code);

    /// <summary>
    /// Gets all the roles using sorting by position (ascending).
    /// </summary>
    /// <returns>Found roles.</returns>
    IEnumerable<Role> All();

    /// <summary>
    /// Gets all the roles using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The role property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of roles that should be skipped.</param>
    /// <param name="take">The number of roles that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found roles using the given filtering, sorting, and paging.</returns>
    IEnumerable<Role> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the role.
    /// </summary>
    /// <param name="role">The role to create.</param>
    void Create(Role role);

    /// <summary>
    /// Edits the role.
    /// </summary>
    /// <param name="role">The role to edit.</param>
    void Edit(Role role);

    /// <summary>
    /// Deletes the role specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the role to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the role.
    /// </summary>
    /// <param name="role">The role to delete.</param>
    void Delete(Role role);

    /// <summary>
    /// Counts the number of the roles with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of roles found.</returns>
    int Count(string filter);
  }
}