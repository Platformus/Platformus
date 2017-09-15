// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="ICompletedFieldRepository"/> interface and represents the repository
  /// for manipulating the <see cref="CompletedField"/> entities in the context of SQLite database.
  /// </summary>
  public class CompletedFieldRepository : RepositoryBase<CompletedField>, ICompletedFieldRepository
  {
    public IEnumerable<CompletedField> FilteredByCompletedFormId(int completedFormId)
    {
      return this.dbSet.AsNoTracking().Where(cf => cf.CompletedFormId == completedFormId).OrderBy(cf => cf.Id);
    }

    public void Create(CompletedField completedField)
    {
      this.dbSet.Add(completedField);
    }
  }
}