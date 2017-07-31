// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
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
  public class ObjectRepository : RepositoryBase<Object>, IObjectRepository
  {
    public Object WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(o => o.Id == id);
    }

    public IEnumerable<Object> All()
    {
      return this.dbSet.OrderBy(o => o.Id);
    }

    public IEnumerable<Object> FilteredByClassId(int classId)
    {
      return this.dbSet.Where(o => o.ClassId == classId).OrderBy(o => o.Id);
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
          CREATE TEMP TABLE TempDictionaries (Id INT PRIMARY KEY);
          INSERT INTO TempDictionaries SELECT StringValueId FROM Properties WHERE ObjectId = {0};
          DELETE FROM Properties WHERE ObjectId = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM TempDictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM TempDictionaries);
          DELETE FROM Relations WHERE PrimaryId = {0} OR ForeignId = {0};
        ",
        @object.Id
      );

      this.dbSet.Remove(@object);
    }
  }
}