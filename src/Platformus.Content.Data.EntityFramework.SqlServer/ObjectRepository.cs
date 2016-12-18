// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;

namespace Platformus.Content.Data.EntityFramework.SqlServer
{
  public class ObjectRepository : RepositoryBase<Object>, IObjectRepository
  {
    public Object WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(o => o.Id == id);
    }

    public Object WithUrl(string url)
    {
      return this.dbSet.FirstOrDefault(o => string.Equals(o.Url, url, System.StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Object> All()
    {
      return this.dbSet.OrderBy(o => o.Url);
    }

    public IEnumerable<Object> FilteredByClassId(int classId)
    {
      return this.dbSet.Where(o => o.ClassId == classId);
    }

    public IEnumerable<Object> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take)
    {
      return this.dbSet.Where(o => o.ClassId == classId).OrderBy(o => o.Url).Skip(skip).Take(take);
    }

    public IEnumerable<Object> Primary(int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM Objects WHERE Id IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {0})", objectId);
    }

    public IEnumerable<Object> Primary(int memberId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM Objects WHERE Id IN (SELECT PrimaryId FROM Relations WHERE MemberId = {0} AND ForeignId = {1})", memberId, objectId);
    }

    public IEnumerable<Object> Foreign(int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM Objects WHERE Id IN (SELECT ForeignId FROM Relations WHERE PrimaryId = {0})", objectId);
    }

    public IEnumerable<Object> Foreign(int memberId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM Objects WHERE Id IN (SELECT ForeignId FROM Relations WHERE MemberId = {0} AND PrimaryId = {1})", memberId, objectId);
    }

    public IEnumerable<Object> Standalone()
    {
      return this.dbSet.FromSql("SELECT * FROM Objects WHERE ClassId IN (SELECT Id FROM Classes WHERE IsStandalone IS NOT NULL)");
    }

    public void Create(Object @object)
    {
      this.dbSet.Add(@object);
    }

    public void Edit(Object @object)
    {
      this.storageContext.Entry(@object).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Object @object)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM CachedObjects WHERE ObjectId = {0};
          CREATE TABLE #Dictionaries (Id INT PRIMARY KEY);
          INSERT INTO #Dictionaries SELECT HtmlId FROM Properties WHERE ObjectId = {0};
          DELETE FROM Properties WHERE ObjectId = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Relations WHERE PrimaryId = {0} OR ForeignId = {0};
        ",
        @object.Id
      );

      this.dbSet.Remove(@object);
    }

    public int CountByClassId(int classId)
    {
      return this.dbSet.Count(o => o.ClassId == classId);
    }
  }
}