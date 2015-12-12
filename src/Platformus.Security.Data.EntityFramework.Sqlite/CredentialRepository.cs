// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.Data.Entity;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Data.EntityFramework.Sqlite
{
  public class CredentialRepository : RepositoryBase<Credential>, ICredentialRepository
  {
    public Credential WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(c => c.Id == id);
    }

    public Credential WithCredentialTypeIdAndIdentifierAndSecret(int credentialTypeId, string identifier, string secret)
    {
      return this.dbSet.FirstOrDefault(c => c.CredentialTypeId == credentialTypeId && string.Equals(c.Identifier, identifier, System.StringComparison.OrdinalIgnoreCase) && c.Secret == secret);
    }

    public IEnumerable<Credential> Range(int userId, string orderBy, string direction, int skip, int take)
    {
      return this.dbSet.Where(c => c.UserId == userId).Skip(skip).Take(take);
    }

    public void Create(Credential credential)
    {
      this.dbSet.Add(credential);
    }

    public void Edit(Credential credential)
    {
      this.dbContext.Entry(credential).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Credential credential)
    {
      this.dbSet.Remove(credential);
    }

    public int CountByUserId(int userId)
    {
      return this.dbSet.Count(c => c.UserId == userId);
    }
  }
}