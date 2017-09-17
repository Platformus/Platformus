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
    /// <summary>
    /// Gets the completed fields filtered by the completed form identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="completedFormId">The unique identifier of the completed form these completed fields belongs to.</param>
    /// <returns>Found completed fields.</returns>
    public IEnumerable<CompletedField> FilteredByCompletedFormId(int completedFormId)
    {
      return this.dbSet.AsNoTracking().Where(cf => cf.CompletedFormId == completedFormId).OrderBy(cf => cf.Id);
    }

    /// <summary>
    /// Creates the completed field.
    /// </summary>
    /// <param name="completedField">The completed field to create.</param>
    public void Create(CompletedField completedField)
    {
      this.dbSet.Add(completedField);
    }
  }
}