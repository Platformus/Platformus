// Copyright © 2017 Dmitry Yegorov. All rights reserved.
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
  public class DataTypeParameterRepository : RepositoryBase<DataTypeParameter>, IDataTypeParameterRepository
  {
    public DataTypeParameter WithKey(int id)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(dtp => dtp.Id == id);
    }

    public IEnumerable<DataTypeParameter> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(dtp => dtp.Name);
    }

    public IEnumerable<DataTypeParameter> FilteredByDataTypeId(int dataTypeId)
    {
      return this.dbSet.AsNoTracking().Where(dtp => dtp.DataTypeId == dataTypeId).OrderBy(dtp => dtp.Name);
    }

    public IEnumerable<DataTypeParameter> FilteredByDataTypeIdRange(int dataTypeId, string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredDataTypeParameters(dbSet.AsNoTracking(), dataTypeId, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public void Create(DataTypeParameter dataTypeParameter)
    {
      this.dbSet.Add(dataTypeParameter);
    }

    public void Edit(DataTypeParameter dataTypeParameter)
    {
      this.storageContext.Entry(dataTypeParameter).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(DataTypeParameter dataTypeParameter)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"DELETE FROM DataTypeParameterValues WHERE DataTypeParameterId = {0};",
        dataTypeParameter.Id
      );

      this.dbSet.Remove(dataTypeParameter);
    }

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