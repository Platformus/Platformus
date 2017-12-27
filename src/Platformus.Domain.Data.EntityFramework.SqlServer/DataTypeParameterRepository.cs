// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IDataTypeParameterRepository"/> interface and represents the repository
  /// for manipulating the <see cref="DataTypeParameter"/> entities in the context of SQL Server database.
  /// </summary>
  public class DataTypeParameterRepository : RepositoryBase<DataTypeParameter>, IDataTypeParameterRepository
  {
    /// <summary>
    /// Gets the data type parameter by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data type parameter.</param>
    /// <returns>Found data type parameter with the given identifier.</returns>
    public DataTypeParameter WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the data type parameter by the data type identifier and code (case insensitive).
    /// </summary>
    /// <param name="dataTypeId">The unique identifier of the data type this data type parameter belongs to.</param>
    /// <param name="code">The unique code of the data type parameter.</param>
    /// <returns>Found data type parameter with the given data type identifier and code.</returns>
    public DataTypeParameter WithDataTypeIdAndCode(int dataTypeId, string code)
    {
      return this.dbSet.FirstOrDefault(dtp => dtp.DataTypeId == dataTypeId && string.Equals(dtp.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets the data type parameters filtered by the data type identifier using sorting by name (ascending).
    /// </summary>
    /// <param name="dataTypeId">The unique identifier of the data type these data type parameters belongs to.</param>
    /// <returns>Found data type parameters.</returns>
    public IEnumerable<DataTypeParameter> FilteredByDataTypeId(int dataTypeId)
    {
      return this.dbSet.AsNoTracking().Where(dtp => dtp.DataTypeId == dataTypeId).OrderBy(dtp => dtp.Name);
    }

    /// <summary>
    /// Gets all the data type parameters filtered by the data type identifier using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="dataTypeId">The unique identifier of the data type these data type parameters belongs to.</param>
    /// <param name="orderBy">The class property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of classes that should be skipped.</param>
    /// <param name="take">The number of classes that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found data type parameters filtered by the data type identifier using the given filtering, sorting, and paging.</returns>
    public IEnumerable<DataTypeParameter> FilteredByDataTypeIdRange(int dataTypeId, string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredDataTypeParameters(dbSet.AsNoTracking(), dataTypeId, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the data type parameter.
    /// </summary>
    /// <param name="dataTypeParameter">The data type parameter to create.</param>
    public void Create(DataTypeParameter dataTypeParameter)
    {
      this.dbSet.Add(dataTypeParameter);
    }

    /// <summary>
    /// Edits the data type parameter.
    /// </summary>
    /// <param name="dataTypeParameter">The data type parameter to edit.</param>
    public void Edit(DataTypeParameter dataTypeParameter)
    {
      this.storageContext.Entry(dataTypeParameter).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the data type parameter specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data type parameter to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the data type parameter.
    /// </summary>
    /// <param name="dataTypeParameter">The data type parameter to delete.</param>
    public void Delete(DataTypeParameter dataTypeParameter)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"DELETE FROM DataTypeParameterValues WHERE DataTypeParameterId = {0};",
        dataTypeParameter.Id
      );

      this.dbSet.Remove(dataTypeParameter);
    }

    /// <summary>
    /// Counts the number of the data type parameters filtered by the data type identifier with the given filtering.
    /// </summary>
    /// <param name="dataTypeId">The unique identifier of the data type these data type parameters belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of data type parameters found.</returns>
    public int CountByDataTypeId(int dataTypeId, string filter)
    {
      return this.GetFilteredDataTypeParameters(dbSet, dataTypeId, filter).Count();
    }

    private IQueryable<DataTypeParameter> GetFilteredDataTypeParameters(IQueryable<DataTypeParameter> dataTypeParameters, int dataTypeId, string filter)
    {
      dataTypeParameters = dataTypeParameters.Where(dtp => dtp.DataTypeId == dataTypeId);

      if (string.IsNullOrEmpty(filter))
        return dataTypeParameters;

      return dataTypeParameters.Where(dtp => dtp.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}