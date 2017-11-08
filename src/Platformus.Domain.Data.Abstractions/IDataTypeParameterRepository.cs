// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="DataTypeParameter"/> entities.
  /// </summary>
  public interface IDataTypeParameterRepository : IRepository
  {
    /// <summary>
    /// Gets the data type parameter by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data type parameter.</param>
    /// <returns>Found data type parameter with the given identifier.</returns>
    DataTypeParameter WithKey(int id);

    /// <summary>
    /// Gets the data type parameter by the data type identifier and code (case insensitive).
    /// </summary>
    /// <param name="dataTypeId">The unique identifier of the data type this data type parameter belongs to.</param>
    /// <param name="code">The unique code of the data type parameter.</param>
    /// <returns>Found data type parameter with the given data type identifier and code.</returns>
    DataTypeParameter WithDataTypeIdAndCode(int dataTypeId, string code);

    /// <summary>
    /// Gets the data type parameters filtered by the data type identifier using sorting by name (ascending).
    /// </summary>
    /// <param name="dataTypeId">The unique identifier of the data type these data type parameters belongs to.</param>
    /// <returns>Found data type parameters.</returns>
    IEnumerable<DataTypeParameter> FilteredByDataTypeId(int dataTypeId);

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
    IEnumerable<DataTypeParameter> FilteredByDataTypeIdRange(int dataTypeId, string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the data type parameter.
    /// </summary>
    /// <param name="dataTypeParameter">The data type parameter to create.</param>
    void Create(DataTypeParameter dataTypeParameter);

    /// <summary>
    /// Edits the data type parameter.
    /// </summary>
    /// <param name="dataTypeParameter">The data type parameter to edit.</param>
    void Edit(DataTypeParameter dataTypeParameter);

    /// <summary>
    /// Deletes the data type parameter specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data type parameter to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the data type parameter.
    /// </summary>
    /// <param name="dataTypeParameter">The data type parameter to delete.</param>
    void Delete(DataTypeParameter dataTypeParameter);

    /// <summary>
    /// Counts the number of the data type parameters filtered by the data type identifier with the given filtering.
    /// </summary>
    /// <param name="dataTypeId">The unique identifier of the data type these data type parameters belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of data type parameters found.</returns>
    int CountByDataTypeId(int dataTypeId, string filter);
  }
}