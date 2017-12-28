// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="IFieldRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Field"/> entities in the context of SQLite database.
  /// </summary>
  public class FieldRepository : RepositoryBase<Field>, IFieldRepository
  {
    /// <summary>
    /// Gets the field by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the field.</param>
    /// <returns>Found field with the given identifier.</returns>
    public Field WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the field by the form identifier and code (case insensitive).
    /// </summary>
    /// <param name="formId">The unique identifier of the form this field belongs to.</param>
    /// <param name="code">The unique code of the field.</param>
    /// <returns>Found field with the given form identifier and code.</returns>
    public Field WithFormIdAndCode(int formId, string code)
    {
      return this.dbSet.FirstOrDefault(f => f.FormId == formId && string.Equals(f.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets the fields filtered by the form identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="formId">The unique identifier of the form these fields belongs to.</param>
    /// <returns>Found fields.</returns>
    public IEnumerable<Field> FilteredByFormId(int formId)
    {
      return this.dbSet.AsNoTracking().Where(f => f.FormId == formId).OrderBy(f => f.Position);
    }

    /// <summary>
    /// Creates the field.
    /// </summary>
    /// <param name="field">The field to create.</param>
    public void Create(Field field)
    {
      this.dbSet.Add(field);
    }

    /// <summary>
    /// Edits the field.
    /// </summary>
    /// <param name="field">The field to edit.</param>
    public void Edit(Field field)
    {
      this.storageContext.Entry(field).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the field specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the field to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the field.
    /// </summary>
    /// <param name="field">The field to delete.</param>
    public void Delete(Field field)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          CREATE TEMP TABLE TempDictionaries (Id INT PRIMARY KEY);
          INSERT INTO TempDictionaries SELECT ValueId FROM FieldOptions WHERE FieldId = {0};
          INSERT INTO TempDictionaries SELECT NameId FROM Fields WHERE Id = {0};
          DELETE FROM FieldOptions WHERE FieldId = {0};
          DELETE FROM Fields WHERE Id = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM TempDictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM TempDictionaries);
        ",
        field.Id
      );
    }
  }
}