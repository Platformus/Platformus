// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="IEndpointPermissionRepository"/> interface and represents the repository
  /// for manipulating the <see cref="EndpointPermission"/> entities in the context of SQLite database.
  /// </summary>
  public class EndpointPermissionRepository : RepositoryBase<EndpointPermission>, IEndpointPermissionRepository
  {
    public EndpointPermission WithKey(int endpointId, int permissionId)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(ep => ep.EndpointId == endpointId && ep.PermissionId == permissionId);
    }

    public IEnumerable<EndpointPermission> FilteredByEndpointId(int endpointId)
    {
      return this.dbSet.AsNoTracking().Where(ep => ep.EndpointId == endpointId);
    }

    public void Create(EndpointPermission endpointPermission)
    {
      this.dbSet.Add(endpointPermission);
    }

    public void Edit(EndpointPermission endpointPermission)
    {
      this.storageContext.Entry(endpointPermission).State = EntityState.Modified;
    }

    public void Delete(int endpointId, int permissionId)
    {
      this.Delete(this.WithKey(endpointId, permissionId));
    }

    public void Delete(EndpointPermission endpointPermission)
    {
      this.dbSet.Remove(endpointPermission);
    }
  }
}