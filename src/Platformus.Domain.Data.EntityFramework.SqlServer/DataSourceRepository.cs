// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.EntityFramework.SqlServer
{
  public class DataSourceRepository : RepositoryBase<DataSource>, IDataSourceRepository
  {
    public DataSource WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(ds => ds.Id == id);
    }

    public IEnumerable<DataSource> FilteredByClassId(int classId)
    {
      return this.dbSet.Where(ds => ds.ClassId == classId).OrderBy(ds => ds.CSharpClassName);
    }

    public IEnumerable<DataSource> FilteredByClassIdInlcudingParent(int classId)
    {
      return this.dbSet.FromSql(
        "SELECT * FROM DataSources WHERE ClassId = {0} OR ClassId IN (SELECT ClassId FROM Classes WHERE Id = {0}) ORDER BY CSharpClassName",
        classId
      );
    }

    public IEnumerable<DataSource> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredDataSources(dbSet, classId, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public void Create(DataSource dataSource)
    {
      this.dbSet.Add(dataSource);
    }

    public void Edit(DataSource dataSource)
    {
      this.storageContext.Entry(dataSource).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(DataSource dataSource)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM CachedObjects WHERE ClassId IN (SELECT ClassId FROM DataSources WHERE Id = {0});
        ",
        dataSource.Id
      );

      this.dbSet.Remove(dataSource);
    }

    public int CountByClassId(int classId, string filter)
    {
      return this.GetFilteredDataSources(dbSet, classId, filter).Count();
    }

    private IQueryable<DataSource> GetFilteredDataSources(IQueryable<DataSource> dataSources, int classId, string filter)
    {
      dataSources = dataSources.Where(ds => ds.ClassId == classId);

      if (string.IsNullOrEmpty(filter))
        return dataSources;

      return dataSources.Where(ds => ds.CSharpClassName.ToLower().Contains(filter.ToLower()));
    }
  }
}