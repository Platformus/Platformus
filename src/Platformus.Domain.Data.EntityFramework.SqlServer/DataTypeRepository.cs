// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.SqlServer
{
  public class DataTypeRepository : RepositoryBase<DataType>, IDataTypeRepository
  {
    public DataType WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(dt => dt.Id == id);
    }

    public IEnumerable<DataType> All()
    {
      return this.dbSet.OrderBy(dt => dt.Position);
    }

    public IEnumerable<DataType> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredDataTypes(dbSet, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public void Create(DataType dataType)
    {
      this.dbSet.Add(dataType);
    }

    public void Edit(DataType dataType)
    {
      this.storageContext.Entry(dataType).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(DataType dataType)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM DataTypeParameterValues WHERE DataTypeParameterId IN (SELECT Id FROM DataTypeParameters WHERE DataTypeId = {0});
          DELETE FROM DataTypeParameters WHERE DataTypeId = {0};
          DELETE FROM SerializedObjects WHERE ObjectId IN (SELECT Id FROM Objects WHERE ClassId IN (SELECT ClassId FROM Members WHERE PropertyDataTypeId = {0}));
          CREATE TABLE #Dictionaries (Id INT PRIMARY KEY);
          INSERT INTO #Dictionaries SELECT StringValueId FROM Properties WHERE MemberId IN (SELECT Id FROM Members WHERE PropertyDataTypeId = {0});
          DELETE FROM Properties WHERE MemberId IN (SELECT Id FROM Members WHERE PropertyDataTypeId = {0});
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Members WHERE PropertyDataTypeId = {0};
        ",
        dataType.Id
      );

      this.dbSet.Remove(dataType);
    }

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