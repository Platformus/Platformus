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
  /// Implements the <see cref="IFieldTypeRepository"/> interface and represents the repository
  /// for manipulating the <see cref="FieldType"/> entities in the context of SQLite database.
  /// </summary>
  public class FieldTypeRepository : RepositoryBase<FieldType>, IFieldTypeRepository
  {
    /// <summary>
    /// Gets the field type by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the field type.</param>
    /// <returns>Found field type with the given identifier.</returns>
    public FieldType WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets all the field types using sorting by position (ascending).
    /// </summary>
    /// <returns>Found field types.</returns>
    public IEnumerable<FieldType> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(ft => ft.Position);
    }
  }
}