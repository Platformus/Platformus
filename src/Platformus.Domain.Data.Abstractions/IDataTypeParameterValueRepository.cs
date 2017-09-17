// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="DataTypeParameterValue"/> entities.
  /// </summary>
  public interface IDataTypeParameterValueRepository : IRepository
  {
    /// <summary>
    /// Gets the data type parameter value by the data type parameter identifier and member identifier.
    /// </summary>
    /// <param name="dataTypeParameterId">The unique identifier of the data type parameter this data type parameter value belongs to.</param>
    /// <param name="memberId">The unique identifier of the member this data type parameter value is related to.</param>
    /// <returns>Found data type parameter value with the given data type parameter identifier and member identifier.</returns>
    DataTypeParameterValue WithDataTypeParameterIdAndMemberId(int dataTypeParameterId, int memberId);

    /// <summary>
    /// Creates the data type parameter value.
    /// </summary>
    /// <param name="dataTypeParameterValue">The data type parameter value to create.</param>
    void Create(DataTypeParameterValue dataTypeParameterValue);

    /// <summary>
    /// Edits the data type parameter value.
    /// </summary>
    /// <param name="dataTypeParameterValue">The data type parameter value to edit.</param>
    void Edit(DataTypeParameterValue dataTypeParameterValue);
  }
}