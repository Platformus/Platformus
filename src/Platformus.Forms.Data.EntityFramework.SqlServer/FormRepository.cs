// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IFormRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Form"/> entities in the context of SQL Server database.
  /// </summary>
  public class FormRepository : RepositoryBase<Form>, IFormRepository
  {
    /// <summary>
    /// Gets the form by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the form.</param>
    /// <returns>Found form with the given identifier.</returns>
    public Form WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the form by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the form.</param>
    /// <returns>Found form with the given code.</returns>
    public Form WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(f => string.Equals(f.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the forms using sorting by name (ascending).
    /// </summary>
    /// <returns>Found forms.</returns>
    public IEnumerable<Form> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(f => f.Name);
    }

    /// <summary>
    /// Creates the form.
    /// </summary>
    /// <param name="form">The form to create.</param>
    public void Create(Form form)
    {
      this.dbSet.Add(form);
    }

    /// <summary>
    /// Edits the form.
    /// </summary>
    /// <param name="form">The form to edit.</param>
    public void Edit(Form form)
    {
      this.storageContext.Entry(form).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the form specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the form to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the form.
    /// </summary>
    /// <param name="form">The form to delete.</param>
    public void Delete(Form form)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM SerializedForms WHERE FormId = {0};
          CREATE TABLE #Dictionaries (Id INT PRIMARY KEY);
          INSERT INTO #Dictionaries SELECT ValueId FROM FieldOptions WHERE FieldId IN (SELECT Id FROM Fields WHERE FormId = {0});
          INSERT INTO #Dictionaries SELECT NameId FROM Fields WHERE FormId = {0};
          INSERT INTO #Dictionaries SELECT NameId FROM Forms WHERE Id = {0};
          DELETE FROM FieldOptions WHERE FieldId IN (SELECT Id FROM Fields WHERE FormId = {0});
          DELETE FROM Fields WHERE FormId = {0};
          DELETE FROM Forms WHERE Id = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM #Dictionaries);
        ",
        form.Id
      );
    }
  }
}