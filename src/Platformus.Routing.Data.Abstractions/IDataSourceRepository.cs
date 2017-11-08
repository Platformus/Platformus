// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="DataSource"/> entities.
  /// </summary>
  public interface IDataSourceRepository : IRepository
  {
    /// <summary>
    /// Gets the data source by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data source.</param>
    /// <returns>Found data source with the given identifier.</returns>
    DataSource WithKey(int id);

    /// <summary>
    /// Gets the data source by the endpoint identifier and code (case insensitive).
    /// </summary>
    /// <param name="endpointId">The unique identifier of the endpoint this data source belongs to.</param>
    /// <param name="code">The unique code of the data source.</param>
    /// <returns>Found data source with the given endpoint identifier and code.</returns>
    DataSource WithEndpointIdAndCode(int endpointId, string code);

    /// <summary>
    /// Gets the data sources filtered by the endpoint identifier using sorting by C# class name (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the endpoint these data sources belongs to.</param>
    /// <returns>Found data sources.</returns>
    IEnumerable<DataSource> FilteredByEndpointId(int endpointId);

    /// <summary>
    /// Gets all the data sources filtered by the endpoint identifier using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="endpointId">The unique identifier of the endpoint these data sources belongs to.</param>
    /// <param name="orderBy">The data source property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of data sources that should be skipped.</param>
    /// <param name="take">The number of data sources that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found data sources filtered by the endpoint identifier using the given filtering, sorting, and paging.</returns>
    IEnumerable<DataSource> FilteredByEndpointIdRange(int endpointId, string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the data source.
    /// </summary>
    /// <param name="dataSource">The data source to create.</param>
    void Create(DataSource dataSource);

    /// <summary>
    /// Edits the data source.
    /// </summary>
    /// <param name="dataSource">The data source to edit.</param>
    void Edit(DataSource dataSource);

    /// <summary>
    /// Deletes the data source specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data source to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the data source.
    /// </summary>
    /// <param name="dataSource">The data source to delete.</param>
    void Delete(DataSource dataSource);

    /// <summary>
    /// Counts the number of the data sources filtered by the endpoint identifier with the given filtering.
    /// </summary>
    /// <param name="endpointId">The unique identifier of the endpoint these data sources belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of data sources found.</returns>
    int CountByEndpointId(int endpointId, string filter);
  }
}