// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="ICredentialTypeRepository"/> interface and represents the repository
  /// for manipulating the <see cref="CredentialType"/> entities in the context of SQLite database.
  /// </summary>
  public class CredentialTypeRepository : RepositoryBase<CredentialType>, ICredentialTypeRepository
  {
    public CredentialType WithKey(int id)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(ct => ct.Id == id);
    }

    public CredentialType WithCode(string code)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(ct => string.Equals(ct.Code, code, System.StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<CredentialType> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(ct => ct.Position);
    }
  }
}