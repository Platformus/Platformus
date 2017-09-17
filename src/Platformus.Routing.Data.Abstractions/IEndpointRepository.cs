// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Endpoint"/> entities.
  /// </summary>
  public interface IEndpointRepository : IRepository
  {
    /// <summary>
    /// Gets the endpoint by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the endpoint.</param>
    /// <returns>Found endpoint with the given identifier.</returns>
    Endpoint WithKey(int id);

    /// <summary>
    /// Gets all the endpoints using sorting by position (ascending).
    /// </summary>
    /// <returns>Found endpoints.</returns>
    IEnumerable<Endpoint> All();

    /// <summary>
    /// Gets all the endpoints using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The endpoint property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of endpoints that should be skipped.</param>
    /// <param name="take">The number of endpoints that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found endpoints using the given filtering, sorting, and paging.</returns>
    IEnumerable<Endpoint> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint to create.</param>
    void Create(Endpoint endpoint);

    /// <summary>
    /// Edits the endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint to edit.</param>
    void Edit(Endpoint endpoint);

    /// <summary>
    /// Deletes the endpoint specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the endpoint to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint to delete.</param>
    void Delete(Endpoint endpoint);

    /// <summary>
    /// Counts the number of the endpoints with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of endpoints found.</returns>
    int Count(string filter);
  }
}