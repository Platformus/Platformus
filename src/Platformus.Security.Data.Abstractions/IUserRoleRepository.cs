// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="UserRole"/> entities.
  /// </summary>
  public interface IUserRoleRepository : IRepository
  {
    /// <summary>
    /// Gets the user role by the user identifier and role identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user this user role is related to.</param>
    /// <param name="roleId">The unique identifier of the role this user role is related to.</param>
    /// <returns>Found user role with the given user identifier and role identifier.</returns>
    UserRole WithKey(int userId, int roleId);

    /// <summary>
    /// Gets the user role filtered by the user identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user these user roles belongs to.</param>
    IEnumerable<UserRole> FilteredByUserId(int userId);

    /// <summary>
    /// Creates the user role.
    /// </summary>
    /// <param name="userRole">The user role to create.</param>
    void Create(UserRole userRole);

    /// <summary>
    /// Edits the user role.
    /// </summary>
    /// <param name="userRole">The user role to edit.</param>
    void Edit(UserRole userRole);

    /// <summary>
    /// Deletes the user role specified by the user identifier and role identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user this user role is related to.</param>
    /// <param name="roleId">The unique identifier of the role this user role is related to.</param>
    void Delete(int userId, int roleId);

    /// <summary>
    /// Deletes the user role.
    /// </summary>
    /// <param name="userRole">The user role to delete.</param>
    void Delete(UserRole userRole);
  }
}