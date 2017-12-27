// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Data.EntityFramework.PostgreSql
{
  /// <summary>
  /// Implements the <see cref="IDataSourceRepository"/> interface and represents the repository
  /// for manipulating the <see cref="DataSource"/> entities in the context of PostgreSQL database.
  /// </summary>
  public class DataSourceRepository : RepositoryBase<DataSource>, IDataSourceRepository
  {
    /// <summary>
    /// Gets the data source by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data source.</param>
    /// <returns>Found data source with the given identifier.</returns>
    public DataSource WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the data source by the endpoint identifier and code (case insensitive).
    /// </summary>
    /// <param name="endpointId">The unique identifier of the endpoint this data source belongs to.</param>
    /// <param name="code">The unique code of the data source.</param>
    /// <returns>Found data source with the given endpoint identifier and code.</returns>
    public DataSource WithEndpointIdAndCode(int endpointId, string code)
    {
      return this.dbSet.FirstOrDefault(ds => ds.EndpointId == endpointId && string.Equals(ds.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets the data sources filtered by the endpoint identifier using sorting by C# class name (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the endpoint these data sources belongs to.</param>
    /// <returns>Found data sources.</returns>
    public IEnumerable<DataSource> FilteredByEndpointId(int endpointId)
    {
      return this.dbSet.AsNoTracking().Where(ds => ds.EndpointId == endpointId).OrderBy(ds => ds.CSharpClassName);
    }

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
    public IEnumerable<DataSource> FilteredByEndpointIdRange(int endpointId, string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredDataSources(dbSet.AsNoTracking(), endpointId, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the data source.
    /// </summary>
    /// <param name="dataSource">The data source to create.</param>
    public void Create(DataSource dataSource)
    {
      this.dbSet.Add(dataSource);
    }

    /// <summary>
    /// Edits the data source.
    /// </summary>
    /// <param name="dataSource">The data source to edit.</param>
    public void Edit(DataSource dataSource)
    {
      this.storageContext.Entry(dataSource).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the data source specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data source to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the data source.
    /// </summary>
    /// <param name="dataSource">The data source to delete.</param>
    public void Delete(DataSource dataSource)
    {
      this.dbSet.Remove(dataSource);
    }

    /// <summary>
    /// Counts the number of the data sources filtered by the endpoint identifier with the given filtering.
    /// </summary>
    /// <param name="endpointId">The unique identifier of the endpoint these data sources belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of data sources found.</returns>
    public int CountByEndpointId(int endpointId, string filter)
    {
      return this.GetFilteredDataSources(dbSet, endpointId, filter).Count();
    }

    private IQueryable<DataSource> GetFilteredDataSources(IQueryable<DataSource> dataSources, int endpointId, string filter)
    {
      dataSources = dataSources.Where(ds => ds.EndpointId == endpointId);

      if (string.IsNullOrEmpty(filter))
        return dataSources;

      return dataSources.Where(ds => ds.CSharpClassName.ToLower().Contains(filter.ToLower()));
    }
  }
}
