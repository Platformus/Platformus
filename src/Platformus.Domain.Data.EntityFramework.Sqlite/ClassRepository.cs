// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
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
  /// Implements the <see cref="IClassRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Class"/> entities in the context of SQLite database.
  /// </summary>
  public class ClassRepository : RepositoryBase<Class>, IClassRepository
  {
    /// <summary>
    /// Gets the class by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the class.</param>
    /// <returns>Found class with the given identifier.</returns>
    public Class WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the class by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the class.</param>
    /// <returns>Found class with the given code.</returns>
    public Class WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(c => string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the classes using sorting by name (ascending).
    /// </summary>
    /// <returns>Found classes.</returns>
    public IEnumerable<Class> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(c => c.Name);
    }

    /// <summary>
    /// Gets all the classes using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The class property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of classes that should be skipped.</param>
    /// <param name="take">The number of classes that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found classes using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Class> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredClasses(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Gets the classes filtered by the parent class identifier using sorting by name (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the parent class these classes belongs to.</param>
    /// <returns>Found classes.</returns>
    public IEnumerable<Class> FilteredByClassId(int? classId)
    {
      if (classId == null)
        return this.dbSet.AsNoTracking().FromSql("SELECT * FROM Classes WHERE Id NOT IN (SELECT RelationClassId FROM Members WHERE IsRelationSingleParent IS NOT NULL) AND ClassId IS NULL AND IsAbstract = {0} ORDER BY Name", false);

      return this.dbSet.AsNoTracking().FromSql("SELECT * FROM Classes WHERE Id NOT IN (SELECT RelationClassId FROM Members WHERE IsRelationSingleParent IS NOT NULL) AND ClassId = {0} AND IsAbstract = {1} ORDER BY Name", classId, false);
    }

    /// <summary>
    /// Gets the abstract classes using sorting by name (ascending).
    /// </summary>
    /// <returns>Found abstract classes.</returns>
    public IEnumerable<Class> Abstract()
    {
      return this.dbSet.AsNoTracking().Where(c => c.IsAbstract).OrderBy(c => c.Name);
    }

    /// <summary>
    /// Gets the not abstract classes using sorting by name (ascending).
    /// </summary>
    /// <returns>Found not abstract classes.</returns>
    public IEnumerable<Class> NotAbstract()
    {
      return this.dbSet.AsNoTracking().Where(c => !c.IsAbstract).OrderBy(c => c.Name);
    }

    /// <summary>
    /// Creates the class.
    /// </summary>
    /// <param name="@class">The class to create.</param>
    public void Create(Class @class)
    {
      this.dbSet.Add(@class);
    }

    /// <summary>
    /// Edits the class.
    /// </summary>
    /// <param name="@class">The class to edit.</param>
    public void Edit(Class @class)
    {
      this.storageContext.Entry(@class).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the class specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the class to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the class.
    /// </summary>
    /// <param name="@class">The class to delete.</param>
    public void Delete(Class @class)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM SerializedObjects WHERE ObjectId IN (SELECT Id FROM Objects WHERE ClassId = {0});
          CREATE TEMP TABLE TempDictionaries (Id INT PRIMARY KEY);
          INSERT INTO TempDictionaries SELECT StringValueId FROM Properties WHERE ObjectId IN (SELECT Id FROM Objects WHERE ClassId = {0}) AND StringValueId IS NOT NULL;
          DELETE FROM Properties WHERE ObjectId IN (SELECT Id FROM Objects WHERE ClassId = {0});
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM TempDictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM TempDictionaries);
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

    /// <summary>
    /// Counts the number of the classes with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of classes found.</returns>
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