// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Models;

namespace Platformus.Forms.Data.EntityFramework.SqlServer
{
  public class FormRepository : RepositoryBase<Form>, IFormRepository
  {
    public Form WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(f => f.Id == id);
    }

    public Form WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(f => string.Equals(f.Code, code, System.StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Form> All()
    {
      return this.dbSet.OrderBy(f => f.Name);
    }

    public void Create(Form form)
    {
      this.dbSet.Add(form);
    }

    public void Edit(Form form)
    {
      this.dbContext.Entry(form).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Form form)
    {
      this.dbContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM CachedForms WHERE FormId = {0};
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