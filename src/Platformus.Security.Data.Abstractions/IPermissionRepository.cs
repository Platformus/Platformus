// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Permission"/> entities.
  /// </summary>
  public interface IPermissionRepository : IRepository
  {
    /// <summary>
    /// Gets the permission by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the permission.</param>
    /// <returns>Found permission with the given identifier.</returns>
    Permission WithKey(int id);

    /// <summary>
    /// Gets the permission by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the permission.</param>
    /// <returns>Found permission with the given code.</returns>
    Permission WithCode(string code);

    /// <summary>
    /// Gets all the permissions using sorting by position (ascending).
    /// </summary>
    /// <returns>Found permissions.</returns>
    IEnumerable<Permission> All();

    /// <summary>
    /// Gets all the permissions using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The permission property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of permissions that should be skipped.</param>
    /// <param name="take">The number of permissions that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found permissions using the given filtering, sorting, and paging.</returns>
    IEnumerable<Permission> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the permission.
    /// </summary>
    /// <param name="permission">The permission to create.</param>
    void Create(Permission permission);

    /// <summary>
    /// Edits the permission.
    /// </summary>
    /// <param name="permission">The permission to edit.</param>
    void Edit(Permission permission);

    /// <summary>
    /// Deletes the permission specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the permission to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the permission.
    /// </summary>
    /// <param name="permission">The permission to delete.</param>
    void Delete(Permission permission);

    /// <summary>
    /// Counts the number of the permissions with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of permissions found.</returns>
    int Count(string filter);
  }
}