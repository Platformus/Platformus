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
    /// <summary>
    /// Gets the endpoint permission by the endpoint identifier and permission identifier.
    /// </summary>
    /// <param name="endpointId">The unique identifier of the endpoint this endpoint permission is related to.</param>
    /// <param name="permissionId">The unique identifier of the permission this endpoint permission is related to.</param>
    /// <returns>Found endpoint permission with the given endpoint identifier and permission identifier.</returns>
    public EndpointPermission WithKey(int endpointId, int permissionId)
    {
      return this.dbSet.Find(endpointId, permissionId);
    }

    /// <summary>
    /// Gets the endpoint permissions filtered by the endpoint identifier.
    /// </summary>
    /// <param name="endpointId">The unique identifier of the endpoint these endpoint permissions belongs to.</param>
    /// <returns>Found endpoint permissions.</returns>
    public IEnumerable<EndpointPermission> FilteredByEndpointId(int endpointId)
    {
      return this.dbSet.AsNoTracking().Where(ep => ep.EndpointId == endpointId);
    }

    /// <summary>
    /// Creates the endpoint permission.
    /// </summary>
    /// <param name="endpointPermission">The endpoint permission to create.</param>
    public void Create(EndpointPermission endpointPermission)
    {
      this.dbSet.Add(endpointPermission);
    }

    /// <summary>
    /// Edits the endpoint permission.
    /// </summary>
    /// <param name="endpointPermission">The endpoint permission to edit.</param>
    public void Edit(EndpointPermission endpointPermission)
    {
      this.storageContext.Entry(endpointPermission).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the endpoint permission specified by the endpoint identifier and permission identifier.
    /// </summary>
    /// <param name="endpointId">The unique identifier of the endpoint this endpoint permission is related to.</param>
    /// <param name="permissionId">The unique identifier of the permission this endpoint permission is related to.</param>
    public void Delete(int endpointId, int permissionId)
    {
      this.Delete(this.WithKey(endpointId, permissionId));
    }

    /// <summary>
    /// Deletes the endpoint permission.
    /// </summary>
    /// <param name="endpointPermission">The endpoint permission to delete.</param>
    public void Delete(EndpointPermission endpointPermission)
    {
      this.dbSet.Remove(endpointPermission);
    }
  }
}