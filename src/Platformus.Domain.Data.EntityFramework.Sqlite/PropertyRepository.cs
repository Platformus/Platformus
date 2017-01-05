// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.EntityFramework.Sqlite
{
  public class PropertyRepository : RepositoryBase<Property>, IPropertyRepository
  {
    public Property WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(p => p.Id == id);
    }

    public Property WithObjectIdAndMemberId(int objectId, int memberId)
    {
      return this.dbSet.FirstOrDefault(p => p.ObjectId == objectId && p.MemberId == memberId);
    }

    public IEnumerable<Property> FilteredByObjectId(int objectId)
    {
      return this.dbSet.Where(p => p.ObjectId == objectId);
    }

    public void Create(Property property)
    {
      this.dbSet.Add(property);
    }

    public void Edit(Property property)
    {
      this.storageContext.Entry(property).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Property property)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          CREATE TEMP TABLE TempDictionaries (Id INT PRIMARY KEY);
          INSERT INTO TempDictionaries SELECT HtmlId FROM Properties WHERE Id = {0};
          DELETE FROM Properties WHERE Id = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM TempDictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM TempDictionaries);
        ",
        property.Id
      );
    }
  }
}