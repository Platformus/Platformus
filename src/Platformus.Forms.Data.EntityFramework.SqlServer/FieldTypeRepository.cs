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
  /// Implements the <see cref="IFieldTypeRepository"/> interface and represents the repository
  /// for manipulating the <see cref="FieldType"/> entities in the context of SQL Server database.
  /// </summary>
  public class FieldTypeRepository : RepositoryBase<FieldType>, IFieldTypeRepository
  {
    public FieldType WithKey(int id)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(ft => ft.Id == id);
    }

    public IEnumerable<FieldType> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(ft => ft.Position);
    }
  }
}