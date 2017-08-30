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
  public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
  {
    public UserRole WithKey(int userId, int roleId)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);
    }

    public IEnumerable<UserRole> FilteredByUserId(int userId)
    {
      return this.dbSet.AsNoTracking().Where(ur => ur.UserId == userId);
    }

    public void Create(UserRole userRole)
    {
      this.dbSet.Add(userRole);
    }

    public void Edit(UserRole userRole)
    {
      this.storageContext.Entry(userRole).State = EntityState.Modified;
    }

    public void Delete(int userId, int roleId)
    {
      this.Delete(this.WithKey(userId, roleId));
    }

    public void Delete(UserRole userRole)
    {
      this.dbSet.Remove(userRole);
    }
  }
}