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
    public DataTypeParameterValue WithDataTypeParameterIdAndMemberId(int dataTypeParameterId, int memberId)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(dtpv => dtpv.DataTypeParameterId == dataTypeParameterId && dtpv.MemberId == memberId);
    }

    public void Create(DataTypeParameterValue dataTypeParameterValue)
    {
      this.dbSet.Add(dataTypeParameterValue);
    }

    public void Edit(DataTypeParameterValue dataTypeParameterValue)
    {
      this.storageContext.Entry(dataTypeParameterValue).State = EntityState.Modified;
    }
  }
}