// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Data.EntityFramework.SqlServer
{
  public class RoleRepository : RepositoryBase<Role>, IRoleRepository
  {
    public Role WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Role> All()
    {
      return this.dbSet.OrderBy(r => r.Position);
    }

    public IEnumerable<Role> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredRoles(dbSet, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public void Create(Role role)
    {
      this.dbSet.Add(role);
    }

    public void Edit(Role role)
    {
      this.storageContext.Entry(role).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

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