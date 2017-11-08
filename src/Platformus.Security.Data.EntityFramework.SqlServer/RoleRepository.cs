// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IRoleRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Role"/> entities in the context of SQL Server database.
  /// </summary>
  public class RoleRepository : RepositoryBase<Role>, IRoleRepository
  {
    /// <summary>
    /// Gets the role by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the role.</param>
    /// <returns>Found role with the given identifier.</returns>
    public Role WithKey(int id)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(r => r.Id == id);
    }

    /// <summary>
    /// Gets the role by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the role.</param>
    /// <returns>Found role with the given code.</returns>
    public Role WithCode(string code)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(r => string.Equals(r.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the roles using sorting by position (ascending).
    /// </summary>
    /// <returns>Found roles.</returns>
    public IEnumerable<Role> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(r => r.Position);
    }

    /// <summary>
    /// Gets all the roles using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The role property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of roles that should be skipped.</param>
    /// <param name="take">The number of roles that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found roles using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Role> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredRoles(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the role.
    /// </summary>
    /// <param name="role">The role to create.</param>
    public void Create(Role role)
    {
      this.dbSet.Add(role);
    }

    /// <summary>
    /// Edits the role.
    /// </summary>
    /// <param name="role">The role to edit.</param>
    public void Edit(Role role)
    {
      this.storageContext.Entry(role).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the role specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the role to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the role.
    /// </summary>
    /// <param name="role">The role to delete.</param>
    public void Delete(Role role)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM RolePermissions WHERE RoleId = {0};
          DELETE FROM UserRoles WHERE RoleId = {0};
        ",
        role.Id
      );

      this.dbSet.Remove(role);
    }

    /// <summary>
    /// Counts the number of the roles with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of roles found.</returns>
    public int Count(string filter)
    {
      return this.GetFilteredRoles(dbSet, filter).Count();
    }

    private IQueryable<Role> GetFilteredRoles(IQueryable<Role> roles, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return roles;

      return roles.Where(r => r.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}