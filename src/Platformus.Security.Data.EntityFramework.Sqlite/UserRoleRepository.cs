// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="IUserRoleRepository"/> interface and represents the repository
  /// for manipulating the <see cref="UserRole"/> entities in the context of SQLite database.
  /// </summary>
  public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
  {
    /// <summary>
    /// Gets the user role by the user identifier and role identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user this user role is related to.</param>
    /// <param name="roleId">The unique identifier of the role this user role is related to.</param>
    /// <returns>Found user role with the given user identifier and role identifier.</returns>
    public UserRole WithKey(int userId, int roleId)
    {
      return this.dbSet.Find(userId, roleId);
    }

    /// <summary>
    /// Gets the user role filtered by the user identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user these user roles belongs to.</param>
    public IEnumerable<UserRole> FilteredByUserId(int userId)
    {
      return this.dbSet.AsNoTracking().Where(ur => ur.UserId == userId);
    }

    /// <summary>
    /// Creates the user role.
    /// </summary>
    /// <param name="userRole">The user role to create.</param>
    public void Create(UserRole userRole)
    {
      this.dbSet.Add(userRole);
    }

    /// <summary>
    /// Edits the user role.
    /// </summary>
    /// <param name="userRole">The user role to edit.</param>
    public void Edit(UserRole userRole)
    {
      this.storageContext.Entry(userRole).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the user role specified by the user identifier and role identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user this user role is related to.</param>
    /// <param name="roleId">The unique identifier of the role this user role is related to.</param>
    public void Delete(int userId, int roleId)
    {
      this.Delete(this.WithKey(userId, roleId));
    }

    /// <summary>
    /// Deletes the user role.
    /// </summary>
    /// <param name="userRole">The user role to delete.</param>
    public void Delete(UserRole userRole)
    {
      this.dbSet.Remove(userRole);
    }
  }
}