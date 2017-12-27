// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="IEndpointRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Endpoint"/> entities in the context of SQLite database.
  /// </summary>
  public class EndpointRepository : RepositoryBase<Endpoint>, IEndpointRepository
  {
    /// <summary>
    /// Gets the endpoint by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the endpoint.</param>
    /// <returns>Found endpoint with the given identifier.</returns>
    public Endpoint WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets all the endpoints using sorting by position (ascending).
    /// </summary>
    /// <returns>Found endpoints.</returns>
    public IEnumerable<Endpoint> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(e => e.Position);
    }

    /// <summary>
    /// Gets all the endpoints using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The endpoint property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of endpoints that should be skipped.</param>
    /// <param name="take">The number of endpoints that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found endpoints using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Endpoint> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredEndpoints(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint to create.</param>
    public void Create(Endpoint endpoint)
    {
      this.dbSet.Add(endpoint);
    }

    /// <summary>
    /// Edits the endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint to edit.</param>
    public void Edit(Endpoint endpoint)
    {
      this.storageContext.Entry(endpoint).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the endpoint specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the endpoint to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint to delete.</param>
    public void Delete(Endpoint endpoint)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM EndpointPermissions WHERE EndpointId = {0};
          DELETE FROM DataSources WHERE EndpointId = {0};
        ",
        endpoint.Id
      );

      this.dbSet.Remove(endpoint);
    }

    /// <summary>
    /// Counts the number of the endpoints with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of endpoints found.</returns>
    public int Count(string filter)
    {
      return this.GetFilteredEndpoints(dbSet, filter).Count();
    }

    private IQueryable<Endpoint> GetFilteredEndpoints(IQueryable<Endpoint> endpoints, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return endpoints;

      return endpoints.Where(e => e.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}