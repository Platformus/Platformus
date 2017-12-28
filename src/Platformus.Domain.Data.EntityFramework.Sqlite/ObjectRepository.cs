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
  /// <summary>
  /// Implements the <see cref="IObjectRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Object"/> entities in the context of SQLite database.
  /// </summary>
  public class ObjectRepository : RepositoryBase<Object>, IObjectRepository
  {
    /// <summary>
    /// Gets the object by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the object.</param>
    /// <returns>Found object with the given identifier.</returns>
    public Object WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets all the objects using sorting by identifier (ascending).
    /// </summary>
    /// <returns>Found classes.</returns>
    public IEnumerable<Object> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(o => o.Id);
    }

    /// <summary>
    /// Gets the objects filtered by the class identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these objects belongs to.</param>
    /// <returns>Found objects.</returns>
    public IEnumerable<Object> FilteredByClassId(int classId)
    {
      return this.dbSet.AsNoTracking().Where(o => o.ClassId == classId).OrderBy(o => o.Id);
    }

    /// <summary>
    /// Creates the object.
    /// </summary>
    /// <param name="@object">The object to create.</param>
    public void Create(Object @object)
    {
      this.dbSet.Add(@object);
    }

    /// <summary>
    /// Edits the object.
    /// </summary>
    /// <param name="@object">The object to edit.</param>
    public void Edit(Object @object)
    {
      this.storageContext.Entry(@object).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the object specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the object to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the object.
    /// </summary>
    /// <param name="@object">The object to delete.</param>
    public void Delete(Object @object)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM SerializedObjects WHERE ObjectId = {0};
          CREATE TEMP TABLE TempDictionaries (Id INT PRIMARY KEY);
          INSERT INTO TempDictionaries SELECT StringValueId FROM Properties WHERE ObjectId = {0} AND StringValueId IS NOT NULL;
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