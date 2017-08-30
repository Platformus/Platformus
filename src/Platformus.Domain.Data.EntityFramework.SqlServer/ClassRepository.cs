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
  public class ClassRepository : RepositoryBase<Class>, IClassRepository
  {
    public Class WithKey(int id)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(c => c.Id == id);
    }

    public Class WithCode(string code)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(c => string.Equals(c.Code, code, System.StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Class> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(c => c.Name);
    }

    public IEnumerable<Class> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredClasses(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public IEnumerable<Class> FilteredByClassId(int? classId)
    {
      if (classId == null)
        return this.dbSet.AsNoTracking().FromSql("SELECT * FROM Classes WHERE Id NOT IN (SELECT RelationClassId FROM Members WHERE IsRelationSingleParent IS NOT NULL) AND ClassId IS NULL AND IsAbstract = {0} ORDER BY Name", false);

      return this.dbSet.AsNoTracking().FromSql("SELECT * FROM Classes WHERE Id NOT IN (SELECT RelationClassId FROM Members WHERE IsRelationSingleParent IS NOT NULL) AND ClassId = {0} AND IsAbstract = {1} ORDER BY Name", classId, false);
    }

    public IEnumerable<Class> Abstract()
    {
      return this.dbSet.AsNoTracking().Where(c => c.IsAbstract).OrderBy(c => c.Name);
    }

    public IEnumerable<Class> NotAbstract()
    {
      return this.dbSet.AsNoTracking().Where(c => !c.IsAbstract).OrderBy(c => c.Name);
    }

    public void Create(Class @class)
    {
      this.dbSet.Add(@class);
    }

    public void Edit(Class @class)
    {
      this.storageContext.Entry(@class).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Class @class)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM SerializedObjects WHERE ObjectId IN (SELECT Id FROM Objects WHERE ClassId = {0});
          CREATE TABLE #Dictionaries (Id INT PRIMARY KEY);
          INSERT INTO #Dictionaries SELECT StringValueId FROM Properties WHERE ObjectId IN (SELECT Id FROM Objects WHERE ClassId = {0}) AND StringValueId IS NOT NULL;
          DELETE FROM Properties WHERE ObjectId IN (SELECT Id FROM Objects WHERE ClassId = {0});
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Relations WHERE MemberId IN (SELECT Id FROM Members WHERE ClassId = {0} OR RelationClassId = {0}) OR PrimaryId IN (SELECT Id FROM Objects WHERE ClassId = {0}) OR ForeignId IN (SELECT Id FROM Objects WHERE ClassId = {0});
          DELETE FROM Objects WHERE ClassId = {0};
          DELETE FROM DataTypeParameterValues WHERE MemberId IN (SELECT Id FROM Members WHERE ClassId = {0});
          DELETE FROM Members WHERE ClassId = {0} OR RelationClassId = {0};
          DELETE FROM Tabs WHERE ClassId = {0};
        ",
        @class.Id
      );

      this.dbSet.Remove(@class);
    }

    public int Count(string filter)
    {
      return this.GetFilteredClasses(dbSet, filter).Count();
    }

    private IQueryable<Class> GetFilteredClasses(IQueryable<Class> classes, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return classes;

      return classes.Where(c => c.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}