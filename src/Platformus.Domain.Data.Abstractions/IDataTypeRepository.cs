// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="DataType"/> entities.
  /// </summary>
  public interface IDataTypeRepository : IRepository
  {
    /// <summary>
    /// Gets the data type by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data type.</param>
    /// <returns>Found data type with the given identifier.</returns>
    DataType WithKey(int id);

    /// <summary>
    /// Gets all the data types using sorting by position (ascending).
    /// </summary>
    /// <returns>Found data types.</returns>
    IEnumerable<DataType> All();

    /// <summary>
    /// Gets all the data types using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The data type property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of data types that should be skipped.</param>
    /// <param name="take">The number of data types that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found data types using the given filtering, sorting, and paging.</returns>
    IEnumerable<DataType> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the data type.
    /// </summary>
    /// <param name="dataType">The data type to create.</param>
    void Create(DataType dataType);

    /// <summary>
    /// Edits the data type.
    /// </summary>
    /// <param name="dataType">The data type to edit.</param>
    void Edit(DataType dataType);

    /// <summary>
    /// Deletes the data type specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data type to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the data type.
    /// </summary>
    /// <param name="dataType">The data type to delete.</param>
    void Delete(DataType dataType);

    /// <summary>
    /// Counts the number of the data types with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of data types found.</returns>
    int Count(string filter);
  }
}