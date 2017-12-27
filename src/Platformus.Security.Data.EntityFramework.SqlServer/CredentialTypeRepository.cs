// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="ICredentialTypeRepository"/> interface and represents the repository
  /// for manipulating the <see cref="CredentialType"/> entities in the context of SQL Server database.
  /// </summary>
  public class CredentialTypeRepository : RepositoryBase<CredentialType>, ICredentialTypeRepository
  {
    /// <summary>
    /// Gets the credential type by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the credential type.</param>
    /// <returns>Found credential type with the given identifier.</returns>
    public CredentialType WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the credential type by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the credential type.</param>
    /// <returns>Found credential type with the given code.</returns>
    public CredentialType WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(ct => string.Equals(ct.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the credential types using sorting by position (ascending).
    /// </summary>
    /// <returns>Found credential types.</returns>
    public IEnumerable<CredentialType> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(ct => ct.Position);
    }
  }
}