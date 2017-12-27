// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IDataTypeParameterValueRepository"/> interface and represents the repository
  /// for manipulating the <see cref="DataTypeParameterValue"/> entities in the context of SQL Server database.
  /// </summary>
  public class DataTypeParameterValueRepository : RepositoryBase<DataTypeParameterValue>, IDataTypeParameterValueRepository
  {
    /// <summary>
    /// Gets the data type parameter value by the data type parameter identifier and member identifier.
    /// </summary>
    /// <param name="dataTypeParameterId">The unique identifier of the data type parameter this data type parameter value belongs to.</param>
    /// <param name="memberId">The unique identifier of the member this data type parameter value is related to.</param>
    /// <returns>Found data type parameter value with the given data type parameter identifier and member identifier.</returns>
    public DataTypeParameterValue WithDataTypeParameterIdAndMemberId(int dataTypeParameterId, int memberId)
    {
      return this.dbSet.FirstOrDefault(dtpv => dtpv.DataTypeParameterId == dataTypeParameterId && dtpv.MemberId == memberId);
    }

    /// <summary>
    /// Creates the data type parameter value.
    /// </summary>
    /// <param name="dataTypeParameterValue">The data type parameter value to create.</param>
    public void Create(DataTypeParameterValue dataTypeParameterValue)
    {
      this.dbSet.Add(dataTypeParameterValue);
    }

    /// <summary>
    /// Edits the data type parameter value.
    /// </summary>
    /// <param name="dataTypeParameterValue">The data type parameter value to edit.</param>
    public void Edit(DataTypeParameterValue dataTypeParameterValue)
    {
      this.storageContext.Entry(dataTypeParameterValue).State = EntityState.Modified;
    }
  }
}