// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.EntityFrameworkCore;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;

namespace Platformus.Content.Data.EntityFramework.Sqlite
{
  public class ClassRepository : RepositoryBase<Class>, IClassRepository
  {
    public Class WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(c => c.Id == id);
    }

    public IEnumerable<Class> All()
    {
      return this.dbSet.OrderBy(c => c.Name);
    }

    public IEnumerable<Class> Range(string orderBy, string direction, int skip, int take)
    {
      return this.dbSet.OrderBy(c => c.Name).Skip(skip).Take(take);
    }

    public IEnumerable<Class> Abstract()
    {
      return this.dbSet.Where(c => c.IsAbstract == true).OrderBy(c => c.Name);
    }

    public IEnumerable<Class> Standalone()
    {
      return this.dbSet.FromSql("SELECT * FROM Classes WHERE Id NOT IN (SELECT ClassId FROM Members WHERE IsRelationSingleParent IS NOT NULL) AND IsAbstract IS NULL AND IsStandalone IS NOT NULL ORDER BY Name");
    }

    public IEnumerable<Class> Embedded()
    {
      return this.dbSet.FromSql("SELECT * FROM Classes WHERE Id NOT IN (SELECT ClassId FROM Members WHERE IsRelationSingleParent IS NOT NULL) AND IsAbstract IS NULL AND IsStandalone IS NULL ORDER BY Name");
    }

    public void Create(Class @class)
    {
      this.dbSet.Add(@class);
    }

    public void Edit(Class @class)
    {
      this.dbContext.Entry(@class).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Class @class)
    {
      this.dbContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM CachedObjects WHERE ClassId = {0};
          CREATE TEMP TABLE TempDictionaries (Id INT PRIMARY KEY);
          INSERT INTO TempDictionaries SELECT HtmlId FROM Properties WHERE ObjectId IN (SELECT Id FROM Objects WHERE ClassId = {0});
          DELETE FROM Properties WHERE ObjectId IN (SELECT Id FROM Objects WHERE ClassId = {0});
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM TempDictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM TempDictionaries);
          DELETE FROM Relations WHERE PrimaryId IN (SELECT Id FROM Objects WHERE ClassId = {0}) OR ForeignId IN (SELECT Id FROM Objects WHERE ClassId = {0});
          DELETE FROM Objects WHERE ClassId = {0};
          DELETE FROM Members WHERE ClassId = {0} OR RelationClassId = {0};
          DELETE FROM Tabs WHERE ClassId = {0};
          DELETE FROM DataSources WHERE ClassId = {0};
        ",
        @class.Id
      );

      this.dbSet.Remove(@class);
    }

    public int Count()
    {
      return this.dbSet.Count();
    }
  }
}