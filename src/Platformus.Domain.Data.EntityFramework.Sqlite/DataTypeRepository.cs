// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="IDataTypeRepository"/> interface and represents the repository
  /// for manipulating the <see cref="DataType"/> entities in the context of SQLite database.
  /// </summary>
  public class DataTypeRepository : RepositoryBase<DataType>, IDataTypeRepository
  {
    /// <summary>
    /// Gets the data type by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data type.</param>
    /// <returns>Found data type with the given identifier.</returns>
    public DataType WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets all the data types using sorting by position (ascending).
    /// </summary>
    /// <returns>Found data types.</returns>
    public IEnumerable<DataType> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(dt => dt.Position);
    }

    /// <summary>
    /// Gets all the data types using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The data type property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of data types that should be skipped.</param>
    /// <param name="take">The number of data types that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found data types using the given filtering, sorting, and paging.</returns>
    public IEnumerable<DataType> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredDataTypes(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the data type.
    /// </summary>
    /// <param name="dataType">The data type to create.</param>
    public void Create(DataType dataType)
    {
      this.dbSet.Add(dataType);
    }

    /// <summary>
    /// Edits the data type.
    /// </summary>
    /// <param name="dataType">The data type to edit.</param>
    public void Edit(DataType dataType)
    {
      this.storageContext.Entry(dataType).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the data type specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data type to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the data type.
    /// </summary>
    /// <param name="dataType">The data type to delete.</param>
    public void Delete(DataType dataType)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM DataTypeParameterValues WHERE DataTypeParameterId IN (SELECT Id FROM DataTypeParameters WHERE DataTypeId = {0});
          DELETE FROM DataTypeParameters WHERE DataTypeId = {0};
          DELETE FROM SerializedObjects WHERE ObjectId IN (SELECT Id FROM Objects WHERE ClassId IN (SELECT ClassId FROM Members WHERE PropertyDataTypeId = {0}));
          CREATE TEMP TABLE TempDictionaries (Id INT PRIMARY KEY);
          INSERT INTO TempDictionaries SELECT StringValueId FROM Properties WHERE MemberId IN (SELECT Id FROM Members WHERE PropertyDataTypeId = {0}) AND StringValueId IS NOT NULL;
          DELETE FROM Properties WHERE MemberId IN (SELECT Id FROM Members WHERE PropertyDataTypeId = {0});
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM TempDictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM TempDictionaries);
          DELETE FROM Members WHERE PropertyDataTypeId = {0};
        ",
        dataType.Id
      );

      this.dbSet.Remove(dataType);
    }

    /// <summary>
    /// Counts the number of the data types with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of data types found.</returns>
    public int Count(string filter)
    {
      return this.GetFilteredDataTypes(dbSet, filter).Count();
    }

    private IQueryable<DataType> GetFilteredDataTypes(IQueryable<DataType> dataTypes, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return dataTypes;

      return dataTypes.Where(dt => dt.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}