// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.PostgreSql;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Models;

namespace Platformus.Forms.Data.EntityFramework.PostgreSql
{
  public class CompletedFieldRepository : RepositoryBase<CompletedField>, ICompletedFieldRepository
  {
    public IEnumerable<CompletedField> FilteredByCompletedFormId(int completedFormId)
    {
      return this.dbSet.Where(cf => cf.CompletedFormId == completedFormId).OrderBy(cf => cf.Id);
    }

    public void Create(CompletedField completedField)
    {
      this.dbSet.Add(completedField);
    }
  }
}