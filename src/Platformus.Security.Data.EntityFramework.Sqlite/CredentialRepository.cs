// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
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

    public IEnumerable<Credential> FilteredByUserIdRange(int userId, string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredCredentials(dbSet, userId, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public void Create(Credential credential)
    {
      this.dbSet.Add(credential);
    }

    public void Edit(Credential credential)
    {
      this.storageContext.Entry(credential).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Credential credential)
    {
      this.dbSet.Remove(credential);
    }

    public int CountByUserId(int userId, string filter)
    {
      return this.GetFilteredCredentials(dbSet, userId, filter).Count();
    }

    private IQueryable<Credential> GetFilteredCredentials(IQueryable<Credential> credentials, int userId, string filter)
    {
      credentials = credentials.Where(c => c.UserId == userId);

      if (string.IsNullOrEmpty(filter))
        return credentials;

      return credentials.Where(c => c.Identifier.ToLower().Contains(filter.ToLower()));
    }
  }
}