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
  public class ObjectRepository : RepositoryBase<Object>, IObjectRepository
  {
    public Object WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(o => o.Id == id);
    }

    public Object WithUrl(string url)
    {
      return this.dbSet.FromSql(
        @"
          SELECT * FROM Objects WHERE Id IN
            (SELECT ObjectId FROM Properties WHERE MemberId IN
              (SELECT Id FROM Members WHERE Code = {0}) AND StringValueId IN (SELECT DictionaryId FROM Localizations WHERE Value = {1}))
        ",
        "Url", url
      ).FirstOrDefault();
    }

    public IEnumerable<Object> All()
    {
      return this.dbSet.OrderBy(o => o.Id);
    }

    public IEnumerable<Object> FilteredByClassId(int classId)
    {
      return this.dbSet.Where(o => o.ClassId == classId);
    }

    public IEnumerable<Object> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take)
    {
      return this.dbSet.Where(o => o.ClassId == classId).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public IEnumerable<Object> FilteredByClassIdAndObjectIdRange(int classId, int objectId, string orderBy, string direction, int skip, int take)
    {
      return this.dbSet.Where(o => o.ClassId == classId && o.ForeignRelations.Any(r => r.PrimaryId == objectId)).OrderBy(orderBy, direction).Skip(skip).Take(take);
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
          DELETE FROM SerializedObjects WHERE ObjectId = {0};
          CREATE TABLE #Dictionaries (Id INT PRIMARY KEY);
          INSERT INTO #Dictionaries SELECT StringValueId FROM Properties WHERE ObjectId = {0};
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