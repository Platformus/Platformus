// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.EntityFramework.Sqlite
{
  public class DataTypeParameterValueRepository : RepositoryBase<DataTypeParameterValue>, IDataTypeParameterValueRepository
  {
    public DataTypeParameterValue WithDataTypeParameterIdAndMemberId(int dataTypeParameterId, int memberId)
    {
      return this.dbSet.FirstOrDefault(dtpv => dtpv.DataTypeParameterId == dataTypeParameterId && dtpv.MemberId == memberId);
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