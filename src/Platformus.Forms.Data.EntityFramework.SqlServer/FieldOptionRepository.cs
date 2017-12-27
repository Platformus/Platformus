// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IFieldOptionRepository"/> interface and represents the repository
  /// for manipulating the <see cref="FieldOption"/> entities in the context of SQL Server database.
  /// </summary>
  public class FieldOptionRepository : RepositoryBase<FieldOption>, IFieldOptionRepository
  {
    /// <summary>
    /// Gets the field option by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the field option.</param>
    /// <returns>Found field option with the given identifier.</returns>
    public FieldOption WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the field options filtered by the field identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="fieldId">The unique identifier of the field these field options belongs to.</param>
    /// <returns>Found field options.</returns>
    public IEnumerable<FieldOption> FilteredByFieldId(int fieldId)
    {
      return this.dbSet.AsNoTracking().Where(fo => fo.FieldId == fieldId).OrderBy(fo => fo.Position);
    }

    /// <summary>
    /// Creates the field option.
    /// </summary>
    /// <param name="fieldOption">The field option to create.</param>
    public void Create(FieldOption fieldOption)
    {
      this.dbSet.Add(fieldOption);
    }

    /// <summary>
    /// Edits the field option.
    /// </summary>
    /// <param name="fieldOption">The field option to edit.</param>
    public void Edit(FieldOption fieldOption)
    {
      this.storageContext.Entry(fieldOption).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the field option specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the field option to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the field option.
    /// </summary>
    /// <param name="fieldOption">The field option to delete.</param>
    public void Delete(FieldOption fieldOption)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          CREATE TABLE #Dictionaries (Id INT PRIMARY KEY);
          INSERT INTO #Dictionaries SELECT ValueId FROM FieldOptions WHERE Id = {0};
          DELETE FROM FieldOptions WHERE Id = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM #Dictionaries);
        ",
        fieldOption.Id
      );
    }
  }
}