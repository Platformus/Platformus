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
  public class CompletedFormRepository : RepositoryBase<CompletedForm>, ICompletedFormRepository
  {
    public CompletedForm WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(cf => cf.Id == id);
    }

    public IEnumerable<CompletedForm> Range(int formId, string orderBy, string direction, int skip, int take)
    {
      return this.dbSet.Where(cf => cf.FormId == formId).OrderByDescending(cf => cf.Created).Skip(skip).Take(take);
    }

    public void Create(CompletedForm completedForm)
    {
      this.dbSet.Add(completedForm);
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(CompletedForm completedForm)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM CompletedFields WHERE CompletedFormId = {0};
        ",
        completedForm.Id
      );

      this.dbSet.Remove(completedForm);
    }

    public int Count(int formId)
    {
      return this.dbSet.Count(cf => cf.FormId == formId);
    }
  }
}